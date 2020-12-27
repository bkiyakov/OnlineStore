using AutoMapper;
using OnlineStore.Application.Dtos;
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
        private readonly IMapper mapper;
        public OrderElementService(IOrderElementRepository orderElementRepository,
            IDiscountService discountService,
            IProductService productService,
            IMapper mapper)
        {
            this.orderElementRepository = orderElementRepository;
            this.discountService = discountService;
            this.productService = productService;
            this.mapper = mapper;
        }
        public async Task<OrderElementDto> AddOrderElementAsync(OrderDto order, Guid customerId,
            Guid productId,int productCount)
        {
            var itemPrice = await productService.GetProductPriceByIdAsync(productId);
            var discount = await discountService.GetCustomerDiscountPercentAsync(customerId);
            var totalPrice = itemPrice * ((100 - (decimal)discount) / 100);

            OrderElement orderElement = new OrderElement
            {
                OrderId = order.Id,
                ProductId = productId,
                ItemsCount = productCount,
                ItemPrice = totalPrice
            };

            var addedOrderElement = await orderElementRepository.AddOrdeElementAsync(orderElement);
            var result = mapper.Map<OrderElementDto>(addedOrderElement);

            return result;
        }
    }
}
