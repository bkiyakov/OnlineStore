using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Text;

namespace OnlineStore.Application.Dtos
{
    public class ListOfProductsAndCountsDto
    {
        public IEnumerable<ProductAndCountDto> ProductsIdAndCountsList { get; set; }
    }
}
