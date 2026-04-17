using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.Domain.Entities
{
    public class Student : User
    {
        public string University { get; set; } = string.Empty;
        public string Faculty { get; set; } = string.Empty;
        public int LatePaymentCount { get; set; }

        // Navigation Properties
        public int? ApartmentGroupID { get; set; }
        public ApartmentGroup? ApartmentGroup { get; set; }

        public LifestyleQuestionnaire? LifestyleQuestionnaire { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();

        // Ratings submitted by this student
        public ICollection<RoommateRating> RatingsGiven { get; set; } = new List<RoommateRating>();

        // Ratings received by this student
        public ICollection<RoommateRating> RatingsReceived { get; set; } = new List<RoommateRating>();

        public ICollection<Penalty> Penalties { get; set; } = new List<Penalty>();
    }
}
