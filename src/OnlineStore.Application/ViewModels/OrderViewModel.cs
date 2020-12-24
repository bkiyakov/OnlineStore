using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Text;

namespace OnlineStore.Application.ViewModels
{
    public class OrderViewModel
    {
        [JsonProperty("Products")]
        public IEnumerable<ProductAndCount> ProductsIdAndCountsList { get; set; }
        public class ProductAndCount
        {
            public string ProductId { get; set; }
            public int ProductCount { get; set; }
        }
    }
}
