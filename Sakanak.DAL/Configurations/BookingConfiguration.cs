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
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.BookingID);

            builder.Property(b => b.BookingDate)
                .IsRequired();

            builder.Property(b => b.Status)
                .HasConversion<string>()
                .IsRequired();

            // Relationships
            builder.HasOne(b => b.Student)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Apartment)
                .WithMany(a => a.Bookings)
                .HasForeignKey(b => b.ApartmentID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
