using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.Domain.Entities
{
    public class RoommateRating
    {
        public int RatingID { get; set; }
        public int RaterStudentID { get; set; }
        public int RatedStudentID { get; set; }
        public int Score { get; set; } // 1-5 scale
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public Student RaterStudent { get; set; } = null!;
        public Student RatedStudent { get; set; } = null!;
    }
}
