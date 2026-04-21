using Sakanak.Domain.Enums;

namespace Sakanak.Domain.Entities;

public class Admin : User
{
    public AdminRoleLevel RoleLevel { get; set; }
    public ICollection<Contract> VerifiedContracts { get; set; } = new List<Contract>();
}
