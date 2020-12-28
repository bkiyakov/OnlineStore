using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Application.Dtos
{
    public class OrderElementDto
    {
        public ProductDto Product { get; set; }
        public int ItemsCount { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
