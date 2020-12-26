using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.API.Models
{
    public class OrderElementResponseModel
    {
        public Product Product { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderElementResponseModel(OrderElement orderElement)
        {
            Product = orderElement.Product;
            Count = orderElement.ItemsCount;
            TotalPrice = orderElement.ItemPrice * orderElement.ItemsCount;
        }
    }
}
