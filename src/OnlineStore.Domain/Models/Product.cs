using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineStore.Domain.Models
{
    /// <summary>
    /// Товар
    /// </summary>
    public class Product
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [RegularExpression(@"\d{2}-\d{4}-[A-Z]{2}\d{2}",
            ErrorMessage = "Код товара не соответствует шаблону")]
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [StringLength(30)]
        public string Category { get; set; }
    }
}
