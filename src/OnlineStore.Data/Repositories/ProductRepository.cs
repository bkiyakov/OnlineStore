using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using OnlineStore.Application.Exceptions;

namespace OnlineStore.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext context;
        public ProductRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public async Task<Product> AddProductAsync(Product newProduct)
        {
            var addedProduct = context.Products.Add(newProduct);

            await context.SaveChangesAsync();

            return addedProduct.Entity;
        }

        public async Task DeleteProductByIdAsync(Guid productId)
        {
            Product product = await context.Products.FindAsync(productId);

            if (product == null) throw new NotFoundException();

            context.Products.Remove(product);

            if ((await context.SaveChangesAsync()) < 1) // TODO отлавливать ошибку неудачного обновления
                throw new ApplicationException("Не удалось удалить товар в БД");
        }

        public async Task<IEnumerable<string>> GetAllCategoriesAsync()
        {
            var categories =  await context.Set<Product>().Select(p => p.Category).Distinct().ToListAsync();

            return categories;
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

        public async Task<IEnumerable<Product>> GetProductsInCategoryAsync(string categoryName)
        {
            var products = await context.Products.Where(p => p.Category == categoryName).ToListAsync();

            return products;
        }

        public async Task UpdateProductAsync(Product product)
        {
            Product productFromDb = await context.Products.FindAsync(product.Id);

            if (productFromDb == null) throw new NotFoundException();

            productFromDb.Code = product.Code;
            productFromDb.Name = product.Name;
            productFromDb.Category = product.Category;

            if ((await context.SaveChangesAsync()) < 1) // TODO отлавливать ошибку неудачного обновления
                throw new ApplicationException("Не удалось обновить товар в БД");
        }
    }
}
