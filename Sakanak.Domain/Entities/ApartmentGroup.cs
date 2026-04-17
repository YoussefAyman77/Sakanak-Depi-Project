using Sakanak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.Domain.Entities
{
    public class ApartmentGroup
    {
        public int GroupID { get; set; }
        public int ApartmentID { get; set; }
        public int CurrentMembers { get; set; }
        public int MaxMembers { get; set; }
        public GroupStatus GroupStatus { get; set; }

        // Navigation Properties
        public Apartment Apartment { get; set; } = null!;
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public Chat? Chat { get; set; }
    }
}
