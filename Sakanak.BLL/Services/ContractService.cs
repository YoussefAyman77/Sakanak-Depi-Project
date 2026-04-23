using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sakanak.BLL.DTOs.Contract;
using Sakanak.BLL.DTOs.Common;
using Sakanak.BLL.Exceptions;
using Sakanak.BLL.Interfaces;
using Sakanak.DAL.UnitOfWork;
using Sakanak.Domain.Entities;
using Sakanak.Domain.Enums;

namespace Sakanak.BLL.Services;

public class ContractService : IContractService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ContractService> _logger;

    public ContractService(IUnitOfWork unitOfWork, ILogger<ContractService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<OperationResult<ContractResponseDto>> SubmitContractAsync(CreateContractDto dto)
    {
        try
        {
            _logger.LogInformation("Submitting contract for booking {BookingId}", dto.BookingId);

            var booking = await _unitOfWork.Bookings.GetByIdAsync(dto.BookingId);
            if (booking == null) throw new BookingNotFoundException(dto.BookingId);

            if (booking.Status != BookingStatus.Accepted)
            {
                return OperationResult<ContractResponseDto>.FailureResult("Cannot submit contract unless booking is accepted");
            }

            // Check if contract already exists for this booking
            var existingContract = await _unitOfWork.Contracts.GetByBookingIdAsync(dto.BookingId);
            if (existingContract != null)
            {
                return OperationResult<ContractResponseDto>.FailureResult("A contract already exists for this booking");
            }

            var contract = new Contract
            {
                BookingId = dto.BookingId,
                StudentId = booking.StudentId,
                ApartmentId = booking.ApartmentId,
                LandlordId = booking.Apartment.LandlordId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                SubmittedAt = DateTime.UtcNow,
                Status = ContractStatus.PendingApproval
            };

            await _unitOfWork.Contracts.AddAsync(contract);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<ContractResponseDto>.SuccessResult(MapToResponseDto(contract));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error submitting contract");
            return OperationResult<ContractResponseDto>.FailureResult("An error occurred while submitting the contract");
        }
    }

    public async Task<OperationResult<bool>> ApproveContractAsync(int contractId, int adminId)
    {
        return await UpdateContractStatusAsync(contractId, adminId, ContractStatus.Approved);
    }

    public async Task<OperationResult<bool>> RejectContractAsync(int contractId, int adminId, string reason)
    {
        return await UpdateContractStatusAsync(contractId, adminId, ContractStatus.Rejected);
    }

    public async Task<OperationResult<ContractResponseDto>> GetContractByIdAsync(int contractId)
    {
        var contract = await _unitOfWork.Contracts.GetByIdAsync(contractId);
        if (contract == null) return OperationResult<ContractResponseDto>.FailureResult("Contract not found");

        return OperationResult<ContractResponseDto>.SuccessResult(MapToResponseDto(contract));
    }

    public async Task<OperationResult<IEnumerable<ContractResponseDto>>> GetStudentContractsAsync(int studentId)
    {
        var contracts = await _unitOfWork.Contracts.GetByStudentIdAsync(studentId);
        return OperationResult<IEnumerable<ContractResponseDto>>.SuccessResult(contracts.Select(MapToResponseDto));
    }

    public async Task<OperationResult<IEnumerable<ContractResponseDto>>> GetPendingApprovalContractsAsync()
    {
        var contracts = await _unitOfWork.Contracts.GetPendingApprovalContractsAsync();
        return OperationResult<IEnumerable<ContractResponseDto>>.SuccessResult(contracts.Select(MapToResponseDto));
    }

    public async Task<OperationResult<IEnumerable<ContractResponseDto>>> GetApprovedContractsAsync()
    {
        var contracts = await _unitOfWork.Contracts.GetApprovedContractsAsync();
        return OperationResult<IEnumerable<ContractResponseDto>>.SuccessResult(contracts.Select(MapToResponseDto));
    }

    public async Task<OperationResult<IEnumerable<ContractResponseDto>>> GetActiveContractsAsync()
    {
        var contracts = await _unitOfWork.Contracts.GetActiveContractsAsync();
        return OperationResult<IEnumerable<ContractResponseDto>>.SuccessResult(contracts.Select(MapToResponseDto));
    }

    private async Task<OperationResult<bool>> UpdateContractStatusAsync(int contractId, int adminId, ContractStatus newStatus)
    {
        try
        {
            var contract = await _unitOfWork.Contracts.GetByIdAsync(contractId);
            if (contract == null) throw new ContractNotFoundException(contractId);

            if (contract.Status != ContractStatus.PendingApproval)
            {
                throw new InvalidContractStatusException($"Cannot change status of contract from {contract.Status} to {newStatus}");
            }

            contract.Status = newStatus;
            contract.VerifiedByAdminId = adminId;
            contract.ReviewedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
            return OperationResult<bool>.SuccessResult(true);
        }
        catch (BusinessRuleViolationException ex)
        {
            return OperationResult<bool>.FailureResult(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating contract status");
            return OperationResult<bool>.FailureResult("An error occurred while updating the contract status");
        }
    }

    private static ContractResponseDto MapToResponseDto(Contract contract)
    {
        return new ContractResponseDto
        {
            ContractId = contract.ContractId,
            BookingId = contract.BookingId,
            StudentId = contract.StudentId,
            StudentName = contract.Student?.Name ?? "Unknown",
            ApartmentId = contract.ApartmentId,
            ApartmentName = contract.Apartment?.Address ?? "Unknown",
            LandlordId = contract.LandlordId,
            LandlordName = contract.Landlord?.Name ?? "Unknown",
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            SubmittedAt = contract.SubmittedAt,
            ReviewedAt = contract.ReviewedAt,
            Status = contract.Status
        };
    }
}
