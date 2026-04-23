using System;
using Sakanak.Domain.Enums;

namespace Sakanak.BLL.DTOs.Payment;

public class PaymentResponseDto
{
    public int PaymentId { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public PaymentStatus Status { get; set; }
    public bool IsLate { get; set; }
}
