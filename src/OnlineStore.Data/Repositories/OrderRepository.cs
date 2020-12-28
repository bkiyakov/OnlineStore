using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Exceptions;
using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDbContext context;
        public OrderRepository(StoreDbContext context)
        {
            this.context = context;
        }
        public async Task<Order> AddOrderAsync(Order order)
        {
            var addedOrder = context.Add(order);
            await context.SaveChangesAsync();
            
            return addedOrder.Entity;
        }

        public async Task DeleteOrderByIdAsync(Guid orderId)
        {
            Order order = await context.Orders.FindAsync(orderId);

            if (order == null) throw new NotFoundException();

            context.Orders.Remove(order);

            if ((await context.SaveChangesAsync()) < 1) // TODO отлавливать ошибку неудачного обновления
                throw new ApplicationException("Не удалось удалить заказ в БД");
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToListAsync();

            return orders;
        }

        public async Task<int> GetNewOrderNumberAsync()
        {
            return await context.GetNextOrderNumberAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await context.Orders.FindAsync(orderId);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            Order orderFromDb = await context.Orders.FindAsync(order.Id);

            if (orderFromDb == null) throw new NotFoundException();

            orderFromDb.CustomerId = order.CustomerId;
            orderFromDb.OrderDate = order.OrderDate;
            orderFromDb.ShipmentDate = order.ShipmentDate;
            orderFromDb.Status = order.Status;
            orderFromDb.OrderNumber = order.OrderNumber; // Надо ли?

            if ((await context.SaveChangesAsync()) < 1) // TODO отлавливать ошибку неудачного обновления
                throw new ApplicationException("Не удалось обновить заказ в БД");
        }
    }
}
