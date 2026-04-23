using System.Collections.Generic;
using System.Threading.Tasks;
using Sakanak.BLL.DTOs.Payment;
using Sakanak.BLL.DTOs.Common;

namespace Sakanak.BLL.Interfaces;

public interface IPaymentService
{
    Task<OperationResult<PaymentResponseDto>> CreatePaymentAsync(CreatePaymentDto dto);
    Task<OperationResult<bool>> ProcessPaymentAsync(int paymentId);
    Task<OperationResult<PaymentResponseDto>> GetPaymentByIdAsync(int paymentId);
    Task<OperationResult<IEnumerable<PaymentResponseDto>>> GetStudentPaymentsAsync(int studentId);
    Task<OperationResult<IEnumerable<PaymentResponseDto>>> GetContractPaymentsAsync(int contractId);
    Task<OperationResult<IEnumerable<PaymentResponseDto>>> GetPendingPaymentsAsync();
    Task<OperationResult<IEnumerable<PaymentResponseDto>>> GetOverduePaymentsAsync();
}
