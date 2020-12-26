using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static OnlineStore.Application.ViewModels.OrderViewModel;

namespace OnlineStore.API.Models
{
    public class OrderResponseModel
    {
        public int OrderNumber { get; set; }
        public List<OrderElementResponseModel> OrderElement { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderResponseModel(Order order)
        {
            OrderNumber = order.OrderNumber;
            OrderElement = new List<OrderElementResponseModel>();
            foreach(var orderElement in order.Items)
            {
                var orderElementResponseModel = new OrderElementResponseModel(orderElement);

                OrderElement.Add(orderElementResponseModel);
                TotalPrice += orderElementResponseModel.TotalPrice;
            }
        }
    }
}
