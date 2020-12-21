using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
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

        public IEnumerable<Product> GetAllProducts()
        {
            return context.Set<Product>();
        }

        public Task<Product> GetProductById(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
