using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sakanak.BLL.DTOs.Payment;
using Sakanak.BLL.DTOs.Common;
using Sakanak.BLL.Exceptions;
using Sakanak.BLL.Interfaces;
using Sakanak.DAL.UnitOfWork;
using Sakanak.Domain.Entities;
using Sakanak.Domain.Enums;

namespace Sakanak.BLL.Services;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(IUnitOfWork unitOfWork, ILogger<PaymentService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<OperationResult<PaymentResponseDto>> CreatePaymentAsync(CreatePaymentDto dto)
    {
        try
        {
            _logger.LogInformation("Creating payment for contract {ContractId}", dto.ContractId);

            var contract = await _unitOfWork.Contracts.GetByIdAsync(dto.ContractId);
            if (contract == null) throw new ContractNotFoundException(dto.ContractId);

            if (contract.Status != ContractStatus.Approved)
            {
                return OperationResult<PaymentResponseDto>.FailureResult("Cannot create payment for contract that is not approved");
            }

            var payment = new Payment
            {
                ContractId = dto.ContractId,
                StudentId = contract.StudentId,
                LandlordId = contract.LandlordId,
                ApartmentId = contract.ApartmentId,
                Amount = dto.Amount,
                DueDate = dto.DueDate,
                Status = PaymentStatus.Pending
            };

            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<PaymentResponseDto>.SuccessResult(MapToResponseDto(payment));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating payment");
            return OperationResult<PaymentResponseDto>.FailureResult("An error occurred while creating the payment");
        }
    }

    public async Task<OperationResult<bool>> ProcessPaymentAsync(int paymentId)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var payment = await _unitOfWork.Payments.GetByIdAsync(paymentId);
            if (payment == null) throw new PaymentNotFoundException(paymentId);

            if (payment.Status != PaymentStatus.Pending)
            {
                throw new InvalidPaymentStatusException($"Payment {paymentId} is already {payment.Status}");
            }

            payment.Status = PaymentStatus.Paid;
            payment.PaymentDate = DateTime.UtcNow;

            // Automatic assignment logic
            var contract = await _unitOfWork.Contracts.GetByIdAsync(payment.ContractId);
            if (contract != null)
            {
                contract.Status = ContractStatus.Active;

                var student = await _unitOfWork.Students.GetByIdAsync(payment.StudentId);
                if (student != null)
                {
                    // Find an open group for the apartment
                    var openGroup = await _unitOfWork.ApartmentGroups.GetOpenGroupByApartmentIdAsync(payment.ApartmentId);

                    if (openGroup == null)
                    {
                        var apartment = await _unitOfWork.Apartments.GetByIdAsync(payment.ApartmentId);
                        // Create new group
                        openGroup = new ApartmentGroup
                        {
                            ApartmentId = payment.ApartmentId,
                            MaxMembers = apartment?.TotalSeats ?? 4,
                            GroupStatus = GroupStatus.Open
                        };
                        await _unitOfWork.ApartmentGroups.AddAsync(openGroup);
                        await _unitOfWork.SaveChangesAsync(); // To get GroupId
                    }

                    student.ApartmentGroupId = openGroup.GroupId;
                    
                    var currentMembers = await _unitOfWork.ApartmentGroups.GetCurrentMemberCountAsync(openGroup.GroupId);
                    if (currentMembers + 1 >= openGroup.MaxMembers)
                    {
                        openGroup.GroupStatus = GroupStatus.Full;
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return OperationResult<bool>.SuccessResult(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Error processing payment {PaymentId}", paymentId);
            return OperationResult<bool>.FailureResult("An error occurred while processing the payment");
        }
    }

    public async Task<OperationResult<PaymentResponseDto>> GetPaymentByIdAsync(int paymentId)
    {
        var payment = await _unitOfWork.Payments.GetByIdAsync(paymentId);
        if (payment == null) return OperationResult<PaymentResponseDto>.FailureResult("Payment not found");

        return OperationResult<PaymentResponseDto>.SuccessResult(MapToResponseDto(payment));
    }

    public async Task<OperationResult<IEnumerable<PaymentResponseDto>>> GetStudentPaymentsAsync(int studentId)
    {
        var payments = await _unitOfWork.Payments.GetByStudentIdAsync(studentId);
        return OperationResult<IEnumerable<PaymentResponseDto>>.SuccessResult(payments.Select(MapToResponseDto));
    }

    public async Task<OperationResult<IEnumerable<PaymentResponseDto>>> GetContractPaymentsAsync(int contractId)
    {
        var payments = await _unitOfWork.Payments.GetByContractIdAsync(contractId);
        return OperationResult<IEnumerable<PaymentResponseDto>>.SuccessResult(payments.Select(MapToResponseDto));
    }

    public async Task<OperationResult<IEnumerable<PaymentResponseDto>>> GetPendingPaymentsAsync()
    {
        var payments = await _unitOfWork.Payments.GetPendingPaymentsAsync();
        return OperationResult<IEnumerable<PaymentResponseDto>>.SuccessResult(payments.Select(MapToResponseDto));
    }

    public async Task<OperationResult<IEnumerable<PaymentResponseDto>>> GetOverduePaymentsAsync()
    {
        var payments = await _unitOfWork.Payments.GetOverduePaymentsAsync();
        return OperationResult<IEnumerable<PaymentResponseDto>>.SuccessResult(payments.Select(MapToResponseDto));
    }

    private static PaymentResponseDto MapToResponseDto(Payment payment)
    {
        return new PaymentResponseDto
        {
            PaymentId = payment.PaymentId,
            StudentId = payment.StudentId,
            StudentName = payment.Student?.Name ?? "Unknown",
            Amount = payment.Amount,
            DueDate = payment.DueDate,
            PaymentDate = payment.PaymentDate,
            Status = payment.Status,
            IsLate = payment.IsLate
        };
    }
}
