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
    public class RoommateRatingConfiguration : IEntityTypeConfiguration<RoommateRating>
    {
        public void Configure(EntityTypeBuilder<RoommateRating> builder)
        {
            builder.HasKey(rr => rr.RatingID);

            builder.Property(rr => rr.Score)
                .IsRequired();

            builder.Property(rr => rr.Comment)
                .HasMaxLength(500);

            builder.Property(rr => rr.CreatedAt)
                .IsRequired();

            // Relationships with explicit foreign keys
            builder.HasOne(rr => rr.RaterStudent)
                .WithMany(s => s.RatingsGiven)
                .HasForeignKey(rr => rr.RaterStudentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rr => rr.RatedStudent)
                .WithMany(s => s.RatingsReceived)
                .HasForeignKey(rr => rr.RatedStudentID)
                .OnDelete(DeleteBehavior.Restrict);

            // Composite index to prevent duplicate ratings
            builder.HasIndex(rr => new { rr.RaterStudentID, rr.RatedStudentID })
                .IsUnique();
        }
    }
}
