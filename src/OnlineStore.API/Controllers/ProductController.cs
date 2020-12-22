using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Exceptions;
using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Application.ViewModels;
using OnlineStore.Domain.Models;
using System;
using System.Threading.Tasks;

namespace OnlineStore.API.Controllers
{
    // TODO обработка исключений
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [Route("get-all")]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productRepository.GetAllProducts();

            return Ok(products);
        }

        [Route("get-by-code/{productCode}")]
        [HttpGet]
        public async Task<IActionResult> GetProductByCode(string productCode)
        {
            Product product = await productRepository.GetProductByCodeAsync(productCode);

            return Ok(new ProductViewModel(product));
        }

        [Route("get-by-id/{productId}")]
        [HttpGet]
        public async Task<IActionResult> GetProductById(string productId)
        {
            Guid productGuid = Guid.Parse(productId);

            Product product = await productRepository.GetProductByIdAsync(productGuid);

            return Ok(new ProductViewModel(product));
        }

        //[Authorize(Role="Manager")]
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Product product = new Product
            {
                Code = model.Code,
                Name = model.Name,
                Price = model.Price,
                Category = model.Category
            };

            await productRepository.AddProductAsync(product);

            return Ok();
        }

        [Route("get-categories")]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await productRepository.GetAllCategoriesAsync();

            return Ok(categories);
        }

        //[Authorize(Role="Manager")]
        [Route("delete/{productId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            Guid productGuid = Guid.Parse(productId);

            // TODO Найти более элегантный способ
            try
            {
                await productRepository.DeleteProductByIdAsync(productGuid);
            } catch (NotFoundException ex)
            {
                return NotFound(productGuid);
            } catch (Exception ex)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        //[Authorize(Role="Manager")]
        [Route("update/{productId}")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductViewModel model, string productId)
        {
            Guid productGuid = Guid.Parse(productId);

            Product product = new Product
            {
                Id = productGuid,
                Code = model.Code,
                Name = model.Name,
                Category = model.Category
            };

            // TODO Найти более элегантный способ
            try
            {
                await productRepository.UpdateProductAsync(product);
            }
            catch (NotFoundException ex)
            {
                return NotFound(productGuid);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
