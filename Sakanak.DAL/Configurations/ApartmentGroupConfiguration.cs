using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakanak.Domain.Entities;
using Sakanak.Domain.Enums;

namespace Sakanak.DAL.Configurations;

public class ApartmentGroupConfiguration : IEntityTypeConfiguration<ApartmentGroup>
{
    public void Configure(EntityTypeBuilder<ApartmentGroup> builder)
    {
        builder.HasKey(e => e.GroupId);

        builder.Property(e => e.GroupStatus)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(e => e.ApartmentId);
        builder.HasIndex(e => new { e.ApartmentId, e.GroupStatus })
            .HasFilter($"[{nameof(ApartmentGroup.GroupStatus)}] = '{GroupStatus.Open}'")
            .IsUnique();

        builder.HasOne(e => e.Apartment)
            .WithMany(e => e.ApartmentGroups)
            .HasForeignKey(e => e.ApartmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
