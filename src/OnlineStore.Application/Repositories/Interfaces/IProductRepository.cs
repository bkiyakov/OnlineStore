using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Task<Product> GetProductById(int productId);
        Task<HashSet<string>> GetAllCategories();
        Task AddProductAsync(Product newProduct);
        Task DeleteProductById(int productId);
    }
}
