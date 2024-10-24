using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snai3i_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Configration
{
    internal class ChatEntityTypeConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(a => a.Isdeleted)
               .HasDefaultValue(false);

            builder.HasQueryFilter(a => a.Isdeleted == false);

            builder.HasOne(e => e.sender)
            .WithMany(e => e.SentChats).
            HasForeignKey( e=> e.SenderId)
            .IsRequired().
            OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.receiver)
                .WithMany( e => e.ReceivedChats)
                .HasForeignKey( e => e.ReceiverId)
                .IsRequired().
                OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.SenderId).HasMaxLength(450); 
            builder.Property(e => e.ReceiverId).HasMaxLength(450);
        }
    }
}
