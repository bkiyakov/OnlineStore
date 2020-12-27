using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Application.Services.Interfaces;
using OnlineStore.Application.Dtos;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineStore.Application.Exceptions;

namespace OnlineStore.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderElementService orderElementService;
        private readonly IMapper mapper;
        public OrderService(IOrderRepository orderRepository,
            IOrderElementService orderElementService,
            IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.orderElementService = orderElementService;
            this.mapper = mapper;
        }
        public async Task<OrderDto> AddOrderAsync(ListOfProductsAndCountsDto productsAndCountsList,
            int userId)
        {
            // Получаем id заказчика по id пользователя
            Guid customerId = Guid.NewGuid(); // TODO поменять на реальное получение

            Order order = new Order
            {
                CustomerId = customerId,
                OrderNumber = await orderRepository.GetNewOrderNumberAsync()
            };

            var addedOrder = await orderRepository.AddOrderAsync(order);

            var addedOrderDto = mapper.Map<OrderDto>(addedOrder);

            foreach (var productAndCount in productsAndCountsList.ProductsIdAndCountsList)
            {
                Guid productId = Guid.Parse(productAndCount.ProductId);
                int productCount = productAndCount.ProductCount;

                var addedOrderElementDto = await orderElementService.AddOrderElementAsync(addedOrderDto, customerId, productId, productCount);

                addedOrderDto.Items.Add(addedOrderElementDto);
            }

            return addedOrderDto;
        }

        public async Task ConfirmOrder(Guid orderId, DateTime shipmentDate)
        {
            // Получаем заказ
            var order = await orderRepository.GetOrderByIdAsync(orderId);
            // Проверка на null (если null - NotFound)
            if (order == null) throw new NotFoundException();

            // Проверка дата доставки > даты заказа
            if (shipmentDate.Date < order.OrderDate.Date
                || shipmentDate.Date < DateTime.UtcNow.Date)
                throw new ApplicationException("Дата доставки не может быть меньше даты заказа или сегодня");

            // Изменение и обновление заказа
            order.ShipmentDate = shipmentDate;
            order.SetStatus(Order.StatusInProgress);
            await orderRepository.UpdateOrderAsync(order);
        }
    }
}
