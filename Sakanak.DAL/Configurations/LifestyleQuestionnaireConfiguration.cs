using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Configurations
{
    public class LifestyleQuestionnaireConfiguration : IEntityTypeConfiguration<LifestyleQuestionnaire>
    {
        public void Configure(EntityTypeBuilder<LifestyleQuestionnaire> builder)
        {
            builder.HasKey(lq => lq.QuestionnaireID);

            builder.Property(lq => lq.SleepSchedule)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(lq => lq.IsSmoker)
                .IsRequired();

            builder.Property(lq => lq.HygieneLevel)
                .IsRequired();

            builder.Property(lq => lq.StudyHabits)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(lq => lq.SocialPreference)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(lq => lq.GenderPreference)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(lq => lq.LastUpdated)
                .IsRequired();

            // Relationships
            builder.HasOne(lq => lq.Student)
                .WithOne(s => s.LifestyleQuestionnaire)
                .HasForeignKey<LifestyleQuestionnaire>(lq => lq.StudentID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
