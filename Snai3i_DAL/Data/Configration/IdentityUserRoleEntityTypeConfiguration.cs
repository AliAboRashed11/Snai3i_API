using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Snai3i_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Snai3i_DAL.Data.Configration
{
    internal class IdentityUserRoleEntityTypeConfiguration
    {
        public class CardEntrityTypeConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
        {

            public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
            {
                builder.HasKey(r => new { r.UserId, r.RoleId }); 
            }
        }
    }
}
