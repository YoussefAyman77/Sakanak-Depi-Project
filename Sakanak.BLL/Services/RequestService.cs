using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sakanak.BLL.DTOs.Request;
using Sakanak.BLL.DTOs.Common;
using Sakanak.BLL.Exceptions;
using Sakanak.BLL.Interfaces;
using Sakanak.DAL.UnitOfWork;
using Sakanak.Domain.Entities;
using Sakanak.Domain.Enums;

namespace Sakanak.BLL.Services;

public class RequestService : IRequestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RequestService> _logger;

    public RequestService(IUnitOfWork unitOfWork, ILogger<RequestService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<OperationResult<RequestResponseDto>> CreateRequestAsync(CreateRequestDto dto)
    {
        try
        {
            _logger.LogInformation("Creating upload request for landlord {LandlordId} and apartment {ApartmentId}", dto.LandlordId, dto.ApartmentId);

            var apartment = await _unitOfWork.Apartments.GetByIdAsync(dto.ApartmentId);
            if (apartment == null) return OperationResult<RequestResponseDto>.FailureResult("Apartment not found");

            if (apartment.LandlordId != dto.LandlordId)
            {
                return OperationResult<RequestResponseDto>.FailureResult("Landlord does not own this apartment");
            }

            var request = new Request
            {
                LandlordId = dto.LandlordId,
                ApartmentId = dto.ApartmentId,
                Message = dto.Message,
                Status = RequestStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Requests.AddAsync(request);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<RequestResponseDto>.SuccessResult(MapToResponseDto(request));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating request");
            return OperationResult<RequestResponseDto>.FailureResult("An error occurred while creating the request");
        }
    }

    public async Task<OperationResult<bool>> ApproveRequestAsync(int requestId, int adminId)
    {
        return await UpdateRequestStatusAsync(requestId, adminId, RequestStatus.Approved, true);
    }

    public async Task<OperationResult<bool>> RejectRequestAsync(int requestId, int adminId, string reason)
    {
        return await UpdateRequestStatusAsync(requestId, adminId, RequestStatus.Rejected, true);
    }

    public async Task<OperationResult<bool>> CancelRequestAsync(int requestId, int landlordId)
    {
        return await UpdateRequestStatusAsync(requestId, landlordId, RequestStatus.Cancelled, false);
    }

    public async Task<OperationResult<RequestResponseDto>> GetRequestByIdAsync(int requestId)
    {
        var request = await _unitOfWork.Requests.GetByIdAsync(requestId);
        if (request == null) return OperationResult<RequestResponseDto>.FailureResult("Request not found");

        return OperationResult<RequestResponseDto>.SuccessResult(MapToResponseDto(request));
    }

    public async Task<OperationResult<IEnumerable<RequestResponseDto>>> GetLandlordRequestsAsync(int landlordId)
    {
        var requests = await _unitOfWork.Requests.GetByLandlordIdAsync(landlordId);
        return OperationResult<IEnumerable<RequestResponseDto>>.SuccessResult(requests.Select(MapToResponseDto));
    }

    public async Task<OperationResult<IEnumerable<RequestResponseDto>>> GetPendingRequestsAsync()
    {
        var requests = await _unitOfWork.Requests.GetPendingApartmentRequestsAsync();
        return OperationResult<IEnumerable<RequestResponseDto>>.SuccessResult(requests.Select(MapToResponseDto));
    }

    private async Task<OperationResult<bool>> UpdateRequestStatusAsync(int requestId, int userId, RequestStatus newStatus, bool isAdmin)
    {
        try
        {
            var request = await _unitOfWork.Requests.GetByIdAsync(requestId);
            if (request == null) throw new RequestNotFoundException(requestId);

            if (!isAdmin && request.LandlordId != userId)
            {
                return OperationResult<bool>.FailureResult("Unauthorized landlord action");
            }

            if (request.Status != RequestStatus.Pending)
            {
                return OperationResult<bool>.FailureResult($"Cannot change status of request from {request.Status} to {newStatus}");
            }

            request.Status = newStatus;
            request.ResolvedAt = DateTime.UtcNow;

            if (isAdmin)
            {
                request.ReviewedByAdminId = userId;
                
                if (newStatus == RequestStatus.Approved)
                {
                    var apartment = await _unitOfWork.Apartments.GetByIdAsync(request.ApartmentId);
                    if (apartment != null)
                    {
                        apartment.IsActive = true;
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return OperationResult<bool>.SuccessResult(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating request status");
            return OperationResult<bool>.FailureResult("An error occurred while updating the request status");
        }
    }

    private static RequestResponseDto MapToResponseDto(Request request)
    {
        return new RequestResponseDto
        {
            RequestId = request.RequestId,
            LandlordId = request.LandlordId,
            LandlordName = request.Landlord?.Name ?? "Unknown",
            ApartmentId = request.ApartmentId,
            ApartmentName = request.Apartment?.Address ?? "Unknown",
            Status = request.Status,
            Message = request.Message,
            CreatedAt = request.CreatedAt,
            ResolvedAt = request.ResolvedAt
        };
    }
}
