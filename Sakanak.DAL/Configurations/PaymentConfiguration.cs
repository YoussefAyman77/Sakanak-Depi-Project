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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.PaymentID);

            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.PaymentDate)
                .IsRequired();

            builder.Property(p => p.PaymentStatus)
                .HasConversion<string>()
                .IsRequired();

            // Relationships
            builder.HasOne(p => p.Student)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Landlord)
                .WithMany(l => l.PaymentsReceived)
                .HasForeignKey(p => p.LandlordID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
