using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.API.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.API.Identity.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();
            
            builder.HasData(
                new User
                {
                    // TODO поменять на конфигурационные данные
                    Id = "1a1d111m-1i1n-223b-33bb-444c56df7890",
                    UserName = "manager@onlinestore.com",
                    NormalizedUserName = "manager@onlinestore.com",
                    Email = "manager@onlinestore.com",
                    NormalizedEmail = "manager@onlinestore.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword(null, "passw0rd")
                }
            );
        }
    }
}
