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
    public class ApartmentGroupConfiguration : IEntityTypeConfiguration<ApartmentGroup>
    {
        public void Configure(EntityTypeBuilder<ApartmentGroup> builder)
        {
            builder.HasKey(ag => ag.GroupID);

            builder.Property(ag => ag.CurrentMembers)
                .HasDefaultValue(0);

            builder.Property(ag => ag.MaxMembers)
                .IsRequired();

            builder.Property(ag => ag.GroupStatus)
                .HasConversion<string>()
                .IsRequired();

            // Relationships
            builder.HasOne(ag => ag.Apartment)
                .WithOne(a => a.ApartmentGroup)
                .HasForeignKey<ApartmentGroup>(ag => ag.ApartmentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(ag => ag.Students)
                .WithOne(s => s.ApartmentGroup)
                .HasForeignKey(s => s.ApartmentGroupID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(ag => ag.Chat)
                .WithOne(c => c.ApartmentGroup)
                .HasForeignKey<Chat>(c => c.ApartmentGroupID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
