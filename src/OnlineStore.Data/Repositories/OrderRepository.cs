using Microsoft.EntityFrameworkCore;
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

        public Task DeleteOrderByIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
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

        public Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
