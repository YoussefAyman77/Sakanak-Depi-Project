using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.Domain.Entities
{
    public class Chat
    {
        public int ChatID { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ApartmentGroupID { get; set; }

        // Navigation Properties
        public ApartmentGroup? ApartmentGroup { get; set; }
        public ICollection<User> Participants { get; set; } = new List<User>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
