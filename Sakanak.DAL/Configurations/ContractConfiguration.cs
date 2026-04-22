using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakanak.Domain.Entities;

namespace Sakanak.DAL.Configurations;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(e => e.ContractId);

        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.EndDate)
            .IsRequired();

        builder.Property(e => e.SubmittedAt)
            .IsRequired();

        builder.Property(e => e.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(e => e.BookingId)
            .IsUnique();
        builder.HasIndex(e => new { e.StudentId, e.Status });
        builder.HasIndex(e => new { e.ApartmentId, e.Status });
        builder.HasIndex(e => new { e.LandlordId, e.Status });
        builder.HasIndex(e => e.EndDate);

        builder.HasOne(e => e.Booking)
            .WithOne(e => e.Contract)
            .HasForeignKey<Contract>(e => e.BookingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Student)
            .WithMany(e => e.Contracts)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Apartment)
            .WithMany(e => e.Contracts)
            .HasForeignKey(e => e.ApartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Landlord)
            .WithMany(e => e.Contracts)
            .HasForeignKey(e => e.LandlordId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.VerifiedByAdmin)
            .WithMany(e => e.VerifiedContracts)
            .HasForeignKey(e => e.VerifiedByAdminId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
