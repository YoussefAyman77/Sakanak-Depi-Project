using Sakanak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.Domain.Entities
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int StudentID { get; set; }
        public int ApartmentID { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }

        // Navigation Properties
        public Student Student { get; set; } = null!;
        public Apartment Apartment { get; set; } = null!;
    }
}
