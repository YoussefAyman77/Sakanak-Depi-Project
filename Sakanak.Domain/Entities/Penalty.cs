using Sakanak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.Domain.Entities
{
    public class Penalty
    {
        public int PenaltyID { get; set; }
        public int StudentID { get; set; }
        public int IssuedByAdminID { get; set; }
        public PenaltyReason Reason { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssuedAt { get; set; }
        public bool IsRevoked { get; set; }

        // Navigation Properties
        public Student Student { get; set; } = null!;
        public Admin IssuedByAdmin { get; set; } = null!;
    }
}
