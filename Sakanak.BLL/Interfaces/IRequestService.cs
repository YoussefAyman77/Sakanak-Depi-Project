using System.Collections.Generic;
using System.Threading.Tasks;
using Sakanak.BLL.DTOs.Request;
using Sakanak.BLL.DTOs.Common;

namespace Sakanak.BLL.Interfaces;

public interface IRequestService
{
    Task<OperationResult<RequestResponseDto>> CreateRequestAsync(CreateRequestDto dto);
    Task<OperationResult<bool>> ApproveRequestAsync(int requestId, int adminId);
    Task<OperationResult<bool>> RejectRequestAsync(int requestId, int adminId, string reason);
    Task<OperationResult<bool>> CancelRequestAsync(int requestId, int landlordId);
    Task<OperationResult<RequestResponseDto>> GetRequestByIdAsync(int requestId);
    Task<OperationResult<IEnumerable<RequestResponseDto>>> GetLandlordRequestsAsync(int landlordId);
    Task<OperationResult<IEnumerable<RequestResponseDto>>> GetPendingRequestsAsync();
}
