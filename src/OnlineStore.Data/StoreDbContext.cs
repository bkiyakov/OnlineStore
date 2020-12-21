using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
    }
}
