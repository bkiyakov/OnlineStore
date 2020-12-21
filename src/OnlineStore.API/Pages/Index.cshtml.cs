using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Application.ViewModels;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.API.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductRepository productRepository;

        [BindProperty]
        public ProductViewModel Product { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IProductRepository productRepository)
        {
            _logger = logger;
            this.productRepository = productRepository;
        }

        public void OnGet()
        {

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
