using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.API.Models
{
    public class ProductAddInputModel
    {
        [Required]
        [RegularExpression(@"\d{2}-\d{4}-[A-Z]{2}\d{2}", ErrorMessage = "Код товара не соответствует шаблону")]
        public string Code { get; set; }
        public string Name { get; set; }
        //TODO не работает правильно [RegularExpression(@"^\d+\.\d{0,2}[m,M]{0,1}$", ErrorMessage = "Цена должна иметь формат тип \"130.99\"")]
        [Range(0, 9999999999999999.99)]
        public decimal Price { get; set; }
        [StringLength(30)]
        public string Category { get; set; }
    }
}
