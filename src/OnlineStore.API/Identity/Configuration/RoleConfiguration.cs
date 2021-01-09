using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.API.Identity.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "1a1a111a-1a1a-223b-33bb-444c56df7890",
                    Name = Roles.Manager,
                    NormalizedName = Roles.Manager.ToUpper()
                },
                new IdentityRole
                {
                    Name = Roles.User,
                    NormalizedName = Roles.User.ToUpper()
                }
            );
        }
    }
}
