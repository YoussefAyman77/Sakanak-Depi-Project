using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakanak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Configurations
{
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasKey(a => a.ApartmentID);

            builder.Property(a => a.Address)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.PricePerMonth)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(a => a.TotalSeats)
                .IsRequired();

            builder.Property(a => a.AvailableSeats)
                .IsRequired();

            // Store Amenities as comma-separated string with proper Value Comparer
            builder.Property(a => a.Amenities)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .Metadata.SetValueComparer(
                    new ValueComparer<string[]>(
                        (c1, c2) => c1!.SequenceEqual(c2!),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToArray()));

            builder.Property(a => a.VirtualTourURL)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(a => a.Landlord)
                .WithMany(l => l.Apartments)
                .HasForeignKey(a => a.LandlordID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.ApartmentGroup)
                .WithOne(ag => ag.Apartment)
                .HasForeignKey<ApartmentGroup>(ag => ag.ApartmentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.Bookings)
                .WithOne(b => b.Apartment)
                .HasForeignKey(b => b.ApartmentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Contract)
                .WithOne(c => c.Apartment)
                .HasForeignKey<Contract>(c => c.ApartmentID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
