using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakanak.Domain.Entities;
using Sakanak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakanak.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserID);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(u => u.UserType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(u => u.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(u => u.RegistrationDate)
                .IsRequired();

            // TPH (Table-Per-Hierarchy) configuration
            builder.HasDiscriminator(u => u.UserType)
                .HasValue<Student>(UserType.Student)
                .HasValue<Landlord>(UserType.Landlord)
                .HasValue<Admin>(UserType.Admin);

            // Relationships
            builder.HasMany(u => u.SentMessages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Chats)
                .WithMany(c => c.Participants)
                .UsingEntity(j => j.ToTable("ChatParticipants"));
        }
    }
}
