using System.Collections.Generic;
using System.Threading.Tasks;
using Sakanak.BLL.DTOs.Common;
using Sakanak.Domain.Entities;

namespace Sakanak.BLL.Interfaces;

public interface ILandlordService
{
    Task<OperationResult<Landlord>> GetLandlordByIdAsync(int landlordId);
    Task<OperationResult<IEnumerable<Landlord>>> GetAllLandlordsAsync();
    Task<OperationResult<bool>> SuspendLandlordAsync(int landlordId, int adminId, string reason);
    Task<OperationResult<bool>> ReactivateLandlordAsync(int landlordId, int adminId);
    Task<OperationResult<IEnumerable<Landlord>>> GetVerifiedLandlordsAsync();
}
