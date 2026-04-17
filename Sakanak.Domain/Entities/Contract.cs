using Sakanak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.Domain.Entities
{
    public class Contract
    {
        public int ContractID { get; set; }
        public int ApartmentID { get; set; }
        public int LandlordID { get; set; }
        public int? VerifiedByAdminID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ContractStatus ContractStatus { get; set; }

        // Navigation Properties
        public Apartment Apartment { get; set; } = null!;
        public Landlord Landlord { get; set; } = null!;
        public Admin? VerifiedByAdmin { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
