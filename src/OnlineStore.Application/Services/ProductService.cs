using AutoMapper;
using OnlineStore.Application.Dtos;
using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Application.Services.Interfaces;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ProductDto> AddProductAsync(ProductDto productDto)
        {
            bool productCodeExist = await CheckIfProductCodeExistAsync(productDto.Code);

            if (!productCodeExist)
            {
                var product = mapper.Map<Product>(productDto);
                var addedProduct = await productRepository.AddProductAsync(product);
                var addedProductDto = mapper.Map<ProductDto>(addedProduct);

                return addedProductDto;
            } else
            {
                throw new ApplicationException("Товар с таким кодом уже есть."); //TODO нормальое исключение, нормальная обработка
            }
        }

        public async Task<decimal> GetProductPriceByIdAsync(Guid productId)
        {
            var product = await productRepository.GetProductByIdAsync(productId);

            if (product == null) throw new ApplicationException("Товар не найден");

            return product.Price;
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            bool productCodeExist = true;

            var productFromRepo = await productRepository.GetProductByIdAsync(productDto.Id);

            if (productFromRepo.Code == productDto.Code)
            {
                productCodeExist = false;
            }
            else
            {
                productCodeExist = await CheckIfProductCodeExistAsync(productDto.Code);
            }
            
            if (!productCodeExist)
            {
                var product = mapper.Map<Product>(productDto);
                await productRepository.UpdateProductAsync(product);
            }
            else
            {
                throw new ApplicationException("Товар с таким кодом уже есть."); //TODO нормальое исключение, нормальная обработка
            }
        }

        /// <summary>
        /// Метод проверяет существует ли уже товар с таким кодом.
        /// </summary>
        /// <param name="productCode">Код товара</param>
        /// <returns>true - если товар с таким кодом уже есть, false - если нет.</returns>
        private async Task<bool> CheckIfProductCodeExistAsync(string productCode)
        {
            var product = await productRepository.GetProductByCodeAsync(productCode);

            return product != null ? true : false;
        }

    }
}
