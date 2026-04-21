using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakanak.Domain.Entities;

namespace Sakanak.DAL.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(e => e.BookingId);

        builder.Property(e => e.BookingDate)
            .IsRequired();

        builder.Property(e => e.RequestedStartDate)
            .IsRequired();

        builder.Property(e => e.RequestedEndDate)
            .IsRequired();

        builder.Property(e => e.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(e => new { e.StudentId, e.Status });
        builder.HasIndex(e => new { e.ApartmentId, e.Status });
        builder.HasIndex(e => e.ApartmentGroupId);

        builder.HasOne(e => e.Student)
            .WithMany(e => e.Bookings)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Apartment)
            .WithMany(e => e.Bookings)
            .HasForeignKey(e => e.ApartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.ApartmentGroup)
            .WithMany(e => e.Bookings)
            .HasForeignKey(e => e.ApartmentGroupId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
