using Sakanak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.Domain.Entities
{
    public class Admin : User
    {
        public AdminRoleLevel RoleLevel { get; set; }

        // Navigation Properties
        public ICollection<Contract> VerifiedContracts { get; set; } = new List<Contract>();
        public ICollection<Penalty> IssuedPenalties { get; set; } = new List<Penalty>();
    }
}
