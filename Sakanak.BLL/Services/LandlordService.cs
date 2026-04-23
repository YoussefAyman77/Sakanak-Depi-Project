using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sakanak.BLL.DTOs.Common;
using Sakanak.BLL.Interfaces;
using Sakanak.DAL.UnitOfWork;
using Sakanak.Domain.Entities;
using Sakanak.Domain.Enums;

namespace Sakanak.BLL.Services;

public class LandlordService : ILandlordService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LandlordService> _logger;

    public LandlordService(IUnitOfWork unitOfWork, ILogger<LandlordService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<OperationResult<Landlord>> GetLandlordByIdAsync(int landlordId)
    {
        var landlord = await _unitOfWork.Landlords.GetByIdAsync(landlordId);
        if (landlord == null) return OperationResult<Landlord>.FailureResult("Landlord not found");
        return OperationResult<Landlord>.SuccessResult(landlord);
    }

    public async Task<OperationResult<IEnumerable<Landlord>>> GetAllLandlordsAsync()
    {
        var landlords = await _unitOfWork.Landlords.GetAllAsync();
        return OperationResult<IEnumerable<Landlord>>.SuccessResult(landlords);
    }

    public async Task<OperationResult<bool>> SuspendLandlordAsync(int landlordId, int adminId, string reason)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var landlord = await _unitOfWork.Landlords.GetByIdAsync(landlordId);
            if (landlord == null) return OperationResult<bool>.FailureResult("Landlord not found");

            landlord.Status = UserStatus.Suspended;

            // Affected apartments should be deactivated
            var apartments = await _unitOfWork.Apartments.GetByLandlordIdAsync(landlordId);
            foreach (var apartment in apartments)
            {
                apartment.IsActive = false;
            }

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation("Landlord {LandlordId} suspended by admin {AdminId}. Reason: {Reason}", landlordId, adminId, reason);

            return OperationResult<bool>.SuccessResult(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Error suspending landlord");
            return OperationResult<bool>.FailureResult("An error occurred while suspending the landlord");
        }
    }

    public async Task<OperationResult<bool>> ReactivateLandlordAsync(int landlordId, int adminId)
    {
        try
        {
            var landlord = await _unitOfWork.Landlords.GetByIdAsync(landlordId);
            if (landlord == null) return OperationResult<bool>.FailureResult("Landlord not found");

            landlord.Status = UserStatus.Active;
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Landlord {LandlordId} reactivated by admin {AdminId}", landlordId, adminId);

            return OperationResult<bool>.SuccessResult(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reactivating landlord");
            return OperationResult<bool>.FailureResult("An error occurred while reactivating the landlord");
        }
    }

    public async Task<OperationResult<IEnumerable<Landlord>>> GetVerifiedLandlordsAsync()
    {
        var landlords = await _unitOfWork.Landlords.GetVerifiedLandlordsAsync();
        return OperationResult<IEnumerable<Landlord>>.SuccessResult(landlords);
    }
}
