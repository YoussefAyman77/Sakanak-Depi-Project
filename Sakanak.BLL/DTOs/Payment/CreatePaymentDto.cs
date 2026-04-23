using System;
using System.ComponentModel.DataAnnotations;

namespace Sakanak.BLL.DTOs.Payment;

public class CreatePaymentDto
{
    [Required]
    public int ContractId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }

    [Required]
    public DateTime DueDate { get; set; }
}
