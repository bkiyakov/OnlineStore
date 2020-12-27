using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.API.Models
{
    public class OrderAddInputModel
    {
        [JsonProperty("Products")]
        public IEnumerable<ProductAndCountInputModel> ProductsIdAndCountsList { get; set; }
        public class ProductAndCountInputModel
        {
            public string ProductId { get; set; }
            [Range(0, 999999)]
            public int ProductCount { get; set; }
        }
    }
}
