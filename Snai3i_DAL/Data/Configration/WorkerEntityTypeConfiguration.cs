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
    public class WorkerEntityTypeConfiguration: IEntityTypeConfiguration<Worker>
    {

        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.HasMany(e => e.orders)
                .WithOne(e => e.worker)
                .HasForeignKey(e => e.WorkerId)
            .IsRequired(); 
                


            builder.Property(a => a.Isdeleted)
                .HasDefaultValue(false)
                ;




        }


    }
}
