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
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.Property(a => a.RoleLevel)
                .HasConversion<string>()
                .IsRequired();

            // Relationships
            builder.HasMany(a => a.VerifiedContracts)
                .WithOne(c => c.VerifiedByAdmin)
                .HasForeignKey(c => c.VerifiedByAdminID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(a => a.IssuedPenalties)
                .WithOne(p => p.IssuedByAdmin)
                .HasForeignKey(p => p.IssuedByAdminID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
