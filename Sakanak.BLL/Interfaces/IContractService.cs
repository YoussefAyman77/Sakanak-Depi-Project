using System.Collections.Generic;
using System.Threading.Tasks;
using Sakanak.BLL.DTOs.Contract;
using Sakanak.BLL.DTOs.Common;

namespace Sakanak.BLL.Interfaces;

public interface IContractService
{
    Task<OperationResult<ContractResponseDto>> SubmitContractAsync(CreateContractDto dto);
    Task<OperationResult<bool>> ApproveContractAsync(int contractId, int adminId);
    Task<OperationResult<bool>> RejectContractAsync(int contractId, int adminId, string reason);
    Task<OperationResult<ContractResponseDto>> GetContractByIdAsync(int contractId);
    Task<OperationResult<IEnumerable<ContractResponseDto>>> GetStudentContractsAsync(int studentId);
    Task<OperationResult<IEnumerable<ContractResponseDto>>> GetPendingApprovalContractsAsync();
    Task<OperationResult<IEnumerable<ContractResponseDto>>> GetApprovedContractsAsync();
    Task<OperationResult<IEnumerable<ContractResponseDto>>> GetActiveContractsAsync();
}
