using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Application.ViewModels;
using OnlineStore.Domain.Models;
using System;
using System.Threading.Tasks;

namespace OnlineStore.API.Controllers
{
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
            Guid productIdg = Guid.Parse(productId);

            Product product = await productRepository.GetProductByIdAsync(productIdg);

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
    }
}
