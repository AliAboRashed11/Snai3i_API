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
    internal class SizeEntityTypeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.HasOne(e => e.tool).
                WithMany(e => e.sizes)
                    .HasForeignKey(e => e.ToolId)
                    .IsRequired(true); 


            builder.HasMany(e => e.cards).
                WithOne(e => e.size)
                .HasForeignKey(e => e.SizeId)
                .IsRequired(); 

            builder.Property(a => a.Isdeleted)
                .HasDefaultValue(false);

            builder.HasQueryFilter(a => a.Isdeleted == false);
        }
    }
}
