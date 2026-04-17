using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.Domain.Entities
{
    public class Apartment
    {
        public int ApartmentID { get; set; }
        public int LandlordID { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public decimal PricePerMonth { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public string[] Amenities { get; set; } = Array.Empty<string>();
        public string VirtualTourURL { get; set; } = string.Empty;

        // Navigation Properties
        public Landlord Landlord { get; set; } = null!;
        public ApartmentGroup? ApartmentGroup { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public Contract? Contract { get; set; }
    }
}
