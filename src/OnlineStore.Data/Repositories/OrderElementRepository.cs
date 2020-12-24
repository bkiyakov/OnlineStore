using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repositories
{
    public class OrderElementRepository : IOrderElementRepository
    {
        private readonly StoreDbContext context;

        public OrderElementRepository(StoreDbContext context)
        {
            this.context = context;
        }
        public async Task<OrderElement> AddOrdeElementAsync(OrderElement newOrderElement)
        {
            var addedOrderElement = context.Add(newOrderElement);
            await context.SaveChangesAsync();

            return addedOrderElement.Entity;
        }
    }
}
