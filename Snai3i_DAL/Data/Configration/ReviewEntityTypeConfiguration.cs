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
    public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasOne(e => e.user )
                .WithMany( e => e.reviews)
                .HasForeignKey( e => e.UserId)
                .IsRequired() ;

            builder.HasOne(e => e.worker)
                .WithMany( e=> e.reviews) 
                .HasForeignKey( e => e.WorkerId ) 
                .IsRequired() ;

            builder.Property(a => a.Isdeleted)
            .HasDefaultValue(false);

            builder.HasQueryFilter(a => a.Isdeleted == false);

        }
    }
}
