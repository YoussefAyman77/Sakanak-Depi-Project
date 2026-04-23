using Sakanak.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sakanak.BLL.DTOs.Request;

public class UpdateRequestStatusDto
{
    [Required]
    public RequestStatus Status { get; set; }
    
    public string? Reason { get; set; }
}
