using Sakanak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.Domain.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int StudentID { get; set; }
        public int LandlordID { get; set; }
        public int ApartmentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        // Navigation Properties
        public Student Student { get; set; } = null!;
        public Landlord Landlord { get; set; } = null!;
    }
}
