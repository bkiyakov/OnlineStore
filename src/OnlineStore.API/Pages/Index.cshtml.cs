using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.API.Models;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStore.Application.Dtos;

namespace OnlineStore.API.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductRepository productRepository;

        [BindProperty]
        public ProductAddInputModel Product { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IProductRepository productRepository)
        {
            _logger = logger;
            this.productRepository = productRepository;
        }

        public async Task OnGet()
        {
            var productsFromRepo = await productRepository.GetAllProducts();

            Products = productsFromRepo.Select(p => new ProductDto
            {
                Code = p.Code,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category
            });
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return RedirectToPage(ModelState);

            Product product = new Product
            {
                Code = Product.Code,
                Name = Product.Name,
                Price = Product.Price,
                Category = Product.Category
            };

            await productRepository.AddProductAsync(product);

            return RedirectToPage("Index");
        }
    }
}
