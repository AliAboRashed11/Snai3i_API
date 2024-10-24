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
    internal class CraftEntityTypeConfiguration : IEntityTypeConfiguration<Craft>
    {
        public void Configure(EntityTypeBuilder<Craft> builder)
        {
            builder.HasMany(e => e.workers)
                .WithOne(e => e.craft)
                .HasForeignKey(e => e.CraftId)
                .IsRequired(false); 

            builder.Property(a => a.Isdeleted)
               .HasDefaultValue(false);

            builder.HasQueryFilter(a => a.Isdeleted == false);

            
        }
    }
}
