using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext context;
        public ProductRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public async Task AddProductAsync(Product newProduct)
        {
            context.Add(newProduct);
            await context.SaveChangesAsync();
        }

        public Task DeleteProductById(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<HashSet<string>> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await context.Set<Product>().ToListAsync();
        }

        public async Task<Product> GetProductByCodeAsync(string productCode)
        {
            return await context.Products.FirstOrDefaultAsync(p => p.Code == productCode);
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            return await context.Products.FindAsync(productId);
        }
    }
}
