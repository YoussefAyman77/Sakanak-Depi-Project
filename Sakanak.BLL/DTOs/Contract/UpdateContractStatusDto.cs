using Sakanak.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sakanak.BLL.DTOs.Contract;

public class UpdateContractStatusDto
{
    [Required]
    public ContractStatus Status { get; set; }
    
    public string? Reason { get; set; }
}
