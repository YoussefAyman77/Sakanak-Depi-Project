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
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(c => c.ChatID);

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            // Relationships
            builder.HasOne(c => c.ApartmentGroup)
                .WithOne(ag => ag.Chat)
                .HasForeignKey<Chat>(c => c.ApartmentGroupID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Participants)
                .WithMany(u => u.Chats)
                .UsingEntity(j => j.ToTable("ChatParticipants"));

            builder.HasMany(c => c.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
