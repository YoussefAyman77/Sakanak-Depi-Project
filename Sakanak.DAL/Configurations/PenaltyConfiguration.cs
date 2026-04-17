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
    public class PenaltyConfiguration : IEntityTypeConfiguration<Penalty>
    {
        public void Configure(EntityTypeBuilder<Penalty> builder)
        {
            builder.HasKey(p => p.PenaltyID);

            builder.Property(p => p.Reason)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.IssuedAt)
                .IsRequired();

            builder.Property(p => p.IsRevoked)
                .HasDefaultValue(false);

            // Relationships
            builder.HasOne(p => p.Student)
                .WithMany(s => s.Penalties)
                .HasForeignKey(p => p.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.IssuedByAdmin)
                .WithMany(a => a.IssuedPenalties)
                .HasForeignKey(p => p.IssuedByAdminID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
