using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<decimal> GetProductPriceByIdAsync(Guid productId)
        {
            var product = await productRepository.GetProductByIdAsync(productId);

            if (product == null) throw new ApplicationException("Товар не найден");

            return product.Price;
        }
    }
}
