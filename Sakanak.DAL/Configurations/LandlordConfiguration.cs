using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakanak.Domain.Entities;

namespace Sakanak.DAL.Configurations;

public class LandlordConfiguration : IEntityTypeConfiguration<Landlord>
{
    public void Configure(EntityTypeBuilder<Landlord> builder)
    {
        builder.ToTable("Landlords");

        builder.Property(e => e.VerificationStatus)
            .IsRequired();

        builder.Property(e => e.TotalProperties)
            .HasDefaultValue(0);

        builder.HasIndex(e => e.Email).IsUnique();
        builder.HasIndex(e => e.VerificationStatus);
    }
}
