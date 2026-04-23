using System.ComponentModel.DataAnnotations;

namespace Sakanak.BLL.DTOs.Request;

public class CreateRequestDto
{
    [Required]
    public int LandlordId { get; set; }

    [Required]
    public int ApartmentId { get; set; }

    [Required]
    [StringLength(500)]
    public string Message { get; set; } = string.Empty;
}
