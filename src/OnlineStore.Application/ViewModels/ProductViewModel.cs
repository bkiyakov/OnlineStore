using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineStore.Application.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [RegularExpression(@"\d{2}-\d{4}-[A-Z]{2}\d{2}", ErrorMessage = "Код товара не соответствует шаблону")]
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [StringLength(30)]
        public string Category { get; set; }

        public ProductViewModel()
        {

        }

        public ProductViewModel(Product product)
        {
            Code = product.Code;
            Name = product.Name;
            Price = product.Price;
            Category = product.Category;
        }
    }
}
