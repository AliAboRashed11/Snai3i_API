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
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(e => e.orders)
                .WithOne(e => e.user)
                .HasForeignKey(e => e.UserId)
            .IsRequired(false);

            builder.HasMany(e => e.workers)
                .WithMany(e => e.users); 
           

        }


    }
}
