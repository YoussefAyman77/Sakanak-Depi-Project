using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.Domain.Entities
{
    public class Landlord : User
    {
        public bool VerificationStatus { get; set; }
        public int TotalProperties { get; set; }

        // Navigation Properties
        public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
        public ICollection<Payment> PaymentsReceived { get; set; } = new List<Payment>();
        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
