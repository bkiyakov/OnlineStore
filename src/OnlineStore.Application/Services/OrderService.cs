using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Application.Services.Interfaces;
using OnlineStore.Application.ViewModels;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderElementService orderElementService;
        public OrderService(IOrderRepository orderRepository, IOrderElementService orderElementService)
        {
            this.orderRepository = orderRepository;
            this.orderElementService = orderElementService;
        }
        public async Task<Order> AddOrderAsync(OrderViewModel orderViewModel, int userId)
        {
            // Получаем id заказчика по id пользователя
            Guid customerId = Guid.NewGuid(); // TODO поменять на реальное получение

            Order order = new Order
            {
                CustomerId = customerId,
                OrderNumber = await orderRepository.GetNewOrderNumberAsync()
            };

            var addedOrder = await orderRepository.AddOrderAsync(order);

            foreach(var productAndCount in orderViewModel.ProductsIdAndCountsList)
            {
                Guid productId = Guid.Parse(productAndCount.ProductId);
                int productCount = productAndCount.ProductCount;

                await orderElementService.AddOrderElementAsync(addedOrder, productId, productCount);
            }

            return addedOrder;
        }
    }
}
