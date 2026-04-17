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
    public class LandlordConfiguration : IEntityTypeConfiguration<Landlord>
    {
        public void Configure(EntityTypeBuilder<Landlord> builder)
        {
            builder.Property(l => l.VerificationStatus)
                .HasDefaultValue(false);

            builder.Property(l => l.TotalProperties)
                .HasDefaultValue(0);

            // Relationships
            builder.HasMany(l => l.Apartments)
                .WithOne(a => a.Landlord)
                .HasForeignKey(a => a.LandlordID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(l => l.PaymentsReceived)
                .WithOne(p => p.Landlord)
                .HasForeignKey(p => p.LandlordID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(l => l.Contracts)
                .WithOne(c => c.Landlord)
                .HasForeignKey(c => c.LandlordID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
