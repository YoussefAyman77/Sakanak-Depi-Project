using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.Domain.Entities
{
    public class Message
    {
        public int MessageID { get; set; }
        public int SenderID { get; set; }
        public int ChatID { get; set; }
        public string MessageText { get; set; } = string.Empty;
        public DateTime SentTime { get; set; }
        public bool IsRead { get; set; }

        // Navigation Properties
        public User Sender { get; set; } = null!;
        public Chat Chat { get; set; } = null!;
    }
}
