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
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.University)
                .HasMaxLength(200);

            builder.Property(s => s.Faculty)
                .HasMaxLength(200);

            builder.Property(s => s.LatePaymentCount)
                .HasDefaultValue(0);

            // Relationships
            builder.HasOne(s => s.ApartmentGroup)
                .WithMany(ag => ag.Students)
                .HasForeignKey(s => s.ApartmentGroupID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(s => s.LifestyleQuestionnaire)
                .WithOne(lq => lq.Student)
                .HasForeignKey<LifestyleQuestionnaire>(lq => lq.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Bookings)
                .WithOne(b => b.Student)
                .HasForeignKey(b => b.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Payments)
                .WithOne(p => p.Student)
                .HasForeignKey(p => p.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Contracts)
                .WithMany(c => c.Students)
                .UsingEntity(j => j.ToTable("ContractStudents"));

            builder.HasMany(s => s.RatingsGiven)
                .WithOne(rr => rr.RaterStudent)
                .HasForeignKey(rr => rr.RaterStudentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.RatingsReceived)
                .WithOne(rr => rr.RatedStudent)
                .HasForeignKey(rr => rr.RatedStudentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Penalties)
                .WithOne(p => p.Student)
                .HasForeignKey(p => p.StudentID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
