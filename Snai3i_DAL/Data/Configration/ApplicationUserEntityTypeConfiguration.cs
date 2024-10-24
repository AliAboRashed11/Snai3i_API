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
    internal class ApplicationUserEntityTypeConfiguration
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
    .HasMany(e => e.tools)  // User has many Tools
    .WithMany(e => e.buyers)  // Tool has many Users
    .UsingEntity<Dictionary<string, object>>(  // Use a join table
        "ToolBuyer",  // Join table name
        l => l.HasOne<Tool>()  // Left side: Tool
              .WithMany()
              .HasForeignKey("ToolId")  // Foreign key in join table
              .HasPrincipalKey(t => t.Id)  // ToolId is int
              .OnDelete(DeleteBehavior.Cascade),  // Optional behavior

        r => r.HasOne<ApplicationUser>()  // Right side: ApplicationUser
              .WithMany()
              .HasForeignKey("BuyerId")  // Foreign key in join table
              .HasPrincipalKey(u => u.Id)  // BuyerId is string
              .OnDelete(DeleteBehavior.Cascade),  // Optional behavior

        j =>
        {
            j.HasKey("ToolId", "BuyerId");  // Composite key with different types
            j.ToTable("ToolBuyer");  // Join table name
        });


            builder.Property(a => a.Isdeleted)
            .HasDefaultValue(false);

            builder.HasQueryFilter(a => a.Isdeleted == false);
        }
    }
}
