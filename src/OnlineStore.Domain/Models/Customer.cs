using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineStore.Domain.Models
{
    public class Customer
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"\d{4}-^\d{4}$",
            ErrorMessage = "Код заказчика не соответствует шаблону")]
        public string Code { get; set; }
        public string Address { get; set; }
        public int Discount { get; set; }
    }
}
