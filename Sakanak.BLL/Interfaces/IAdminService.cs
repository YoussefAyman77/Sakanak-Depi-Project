using System.Collections.Generic;
using System.Threading.Tasks;
using Sakanak.BLL.DTOs.Common;
using Sakanak.Domain.Entities;

namespace Sakanak.BLL.Interfaces;

public interface IAdminService
{
    Task<OperationResult<Admin>> GetAdminByIdAsync(int adminId);
    Task<OperationResult<IEnumerable<Admin>>> GetAllAdminsAsync();
    Task<OperationResult<IDictionary<string, object>>> GetDashboardStatsAsync();
    Task<OperationResult<int>> GetPendingRequestsCountAsync();
    Task<OperationResult<int>> GetPendingContractsCountAsync();
}
