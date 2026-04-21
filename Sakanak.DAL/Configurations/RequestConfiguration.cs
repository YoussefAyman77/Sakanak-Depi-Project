using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakanak.Domain.Entities;

namespace Sakanak.DAL.Configurations;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.HasKey(e => e.RequestId);

        builder.Property(e => e.Type)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Message)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.HasIndex(e => new { e.LandlordId, e.Status });
        builder.HasIndex(e => new { e.StudentId, e.Status });
        builder.HasIndex(e => new { e.Type, e.Status });
        builder.HasIndex(e => e.ApartmentId);
        builder.HasIndex(e => e.ContractId);

        builder.HasOne(e => e.Landlord)
            .WithMany(e => e.Requests)
            .HasForeignKey(e => e.LandlordId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Student)
            .WithMany(e => e.Requests)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Apartment)
            .WithMany(e => e.Requests)
            .HasForeignKey(e => e.ApartmentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Contract)
            .WithMany(e => e.Requests)
            .HasForeignKey(e => e.ContractId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
