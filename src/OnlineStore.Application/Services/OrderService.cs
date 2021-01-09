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
        private readonly IOrderElementRepository orderElementRepository;
        private readonly IOrderElementService orderElementService;
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;
        public OrderService(IOrderRepository orderRepository,
            IOrderElementRepository orderElementRepository,
            IOrderElementService orderElementService,
            ICustomerService customerService,
            IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.orderElementRepository = orderElementRepository;
            this.orderElementService = orderElementService;
            this.customerService = customerService;
            this.mapper = mapper;
        }
        public async Task<OrderDto> AddOrderAsync(ListOfProductsAndCountsDto productsAndCountsList,
            string userId)
        {
            // Получаем id заказчика по id пользователя
            Guid customerId = await customerService.GetCustomerIdByUserIdAsync(userId);

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

        public async Task ConfirmOrderAsync(Guid orderId, DateTime shipmentDate)
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

            if (order.Status != OrderStatus.New)
                throw new ApplicationException($"Товар не находится в статусе {OrderStatus.New}");

            order.Status = OrderStatus.InProgress;

            await orderRepository.UpdateOrderAsync(order);
        }
        public async Task CloseOrderAsync(Guid orderId)
        {
            // Получаем заказ
            var order = await orderRepository.GetOrderByIdAsync(orderId);
            // Проверка на null (если null - NotFound)
            if (order == null) throw new NotFoundException();

            if (order.Status != OrderStatus.InProgress)
                throw new ApplicationException($"Товар не находится в статусе {OrderStatus.InProgress}");

            order.Status = OrderStatus.Done;

            await orderRepository.UpdateOrderAsync(order);
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            // Получаем заказ
            var order = await orderRepository.GetOrderByIdAsync(orderId);
            // Проверка на null (если null - NotFound)
            if (order == null) throw new NotFoundException();

            if (order.Status != OrderStatus.New)
                throw new ApplicationException($"Товар не находится в статусе {OrderStatus.New}");

            await orderRepository.DeleteOrderByIdAsync(orderId);

            if (order.Items != null)
            {
                foreach (var orderElement in order.Items)
                {
                    await orderElementRepository.DeleteOrderElementByIdAsync(orderElement.Id);
                }
            }
        }

        public async Task<OrderListPagingDto> GetAllOrdersWithPagingAsync(int pageNumber, int pageSize)
        {
            var ordersFromRepo = await orderRepository.GetAllOrdersWithPagingAsync(pageNumber, pageSize);

            bool hasNext = ordersFromRepo.Count == pageSize; // При остатке заказов, равных ровно нулю будет показывать true

            var orderDtos = mapper.Map<IList<OrderDto>>(ordersFromRepo);

            var orderListPagingDto = new OrderListPagingDto()
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                HasNext = hasNext,
                OrderList = orderDtos
            };

            return orderListPagingDto;
        }
    }
}
