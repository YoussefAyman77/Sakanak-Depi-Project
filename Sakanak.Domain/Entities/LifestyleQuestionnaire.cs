using Sakanak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.Domain.Entities
{
    public class LifestyleQuestionnaire
    {
        public int QuestionnaireID { get; set; }
        public int StudentID { get; set; }
        public SleepSchedule SleepSchedule { get; set; }
        public bool IsSmoker { get; set; }
        public int HygieneLevel { get; set; } // 1-5 scale
        public StudyHabits StudyHabits { get; set; }
        public SocialPreference SocialPreference { get; set; }
        public GenderPreference GenderPreference { get; set; }
        public DateTime LastUpdated { get; set; }

        // Navigation Properties
        public Student Student { get; set; } = null!;
    }
}
