using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Application.Services.Interfaces;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class OrderElementService : IOrderElementService
    {
        private readonly IOrderElementRepository orderElementRepository;
        private readonly IProductService productService;
        private readonly IDiscountService discountService;
        public OrderElementService(IOrderElementRepository orderElementRepository,
            IDiscountService discountService,
            IProductService productService)
        {
            this.orderElementRepository = orderElementRepository;
            this.discountService = discountService;
            this.productService = productService;
        }
        public async Task<OrderElement> AddOrderElementAsync(Order order, Guid productId, int productCount)
        {
            var itemPrice = await productService.GetProductPriceByIdAsync(productId);
            var discount = await discountService.GetCustomerDiscountPercentAsync(order.CustomerId);
            var totalPrice = itemPrice * ((100 - (decimal)discount) / 100);

            OrderElement orderElement = new OrderElement
            {
                OrderId = order.Id,
                ProductId = productId,
                ItemsCount = productCount,
                ItemPrice = totalPrice
            };

            return await orderElementRepository.AddOrdeElementAsync(orderElement);
        }
    }
}
