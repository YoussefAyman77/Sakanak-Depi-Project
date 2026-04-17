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
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(c => c.ContractID);

            builder.Property(c => c.StartDate)
                .IsRequired();

            builder.Property(c => c.EndDate)
                .IsRequired();

            builder.Property(c => c.ContractStatus)
                .HasConversion<string>()
                .IsRequired();

            // Relationships
            builder.HasOne(c => c.Apartment)
                .WithOne(a => a.Contract)
                .HasForeignKey<Contract>(c => c.ApartmentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Landlord)
                .WithMany(l => l.Contracts)
                .HasForeignKey(c => c.LandlordID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.VerifiedByAdmin)
                .WithMany(a => a.VerifiedContracts)
                .HasForeignKey(c => c.VerifiedByAdminID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.Students)
                .WithMany(s => s.Contracts)
                .UsingEntity(j => j.ToTable("ContractStudents"));
        }
    }
}
