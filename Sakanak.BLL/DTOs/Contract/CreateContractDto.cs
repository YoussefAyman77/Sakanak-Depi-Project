using System;
using System.ComponentModel.DataAnnotations;

namespace Sakanak.BLL.DTOs.Contract;

public class CreateContractDto
{
    [Required]
    public int BookingId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }
}
