using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Exceptions;
using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.API.Models;
using System;
using System.Threading.Tasks;
using AutoMapper;
using OnlineStore.Application.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using OnlineStore.Application.Services.Interfaces;

namespace OnlineStore.API.Controllers
{
    // TODO обработка исключений
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductController(IProductRepository productRepository,
            IProductService productService,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.productService = productService;
        }

        [Route("get-all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDto>))]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productRepository.GetAllProducts();
            var productDtos = mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(products);
        }

        [Route("get-by-code/{productCode}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        public async Task<IActionResult> GetProductByCode(string productCode)
        {
            var product = await productRepository.GetProductByCodeAsync(productCode);
            var productDto = mapper.Map<ProductDto>(product);

            return Ok(productDto);
        }

        [Route("get-by-id/{productId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        public async Task<IActionResult> GetProductById(string productId)
        {
            Guid productGuid = Guid.Parse(productId);

            var product = await productRepository.GetProductByIdAsync(productGuid);
            var productDto = mapper.Map<ProductDto>(product);

            return Ok(productDto);
        }

        //[Authorize(Role="Manager")]
        [Route("add")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDto))]
        public async Task<IActionResult> AddProduct([FromBody] ProductAddInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ProductDto productDto = new ProductDto
            {
                Code = model.Code,
                Name = model.Name,
                Price = model.Price,
                Category = model.Category
            };

            var addedProductDto = await productService.AddProductAsync(productDto);

            return CreatedAtAction(nameof(GetProductById),
                new { productId = addedProductDto.Id },
                addedProductDto);
        }

        [Route("get-categories")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await productRepository.GetAllCategoriesAsync();

            return Ok(categories);
        }

        //[Authorize(Role="Manager")]
        [Route("delete/{productId}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductAddInputModel model, string productId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Guid productGuid = Guid.Parse(productId);

            ProductDto productDto = new ProductDto
            {
                Id = productGuid,
                Code = model.Code,
                Name = model.Name,
                Price = model.Price,
                Category = model.Category
            };

            await productService.UpdateProductAsync(productDto);

            return Ok();
        }

        [HttpGet]
        [Route("get-in-category/{categoryName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDto>))]
        public async Task<IActionResult> GetProductsInCategory(string categoryName)
        {
            var products = await productRepository.GetProductsInCategoryAsync(categoryName);
            var productDtos = mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(productDtos);
        }
    }
}
