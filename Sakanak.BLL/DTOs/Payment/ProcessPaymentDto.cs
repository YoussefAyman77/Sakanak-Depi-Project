using System;
using System.ComponentModel.DataAnnotations;

namespace Sakanak.BLL.DTOs.Payment;

public class ProcessPaymentDto
{
    [Required]
    public int PaymentId { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; }
}
