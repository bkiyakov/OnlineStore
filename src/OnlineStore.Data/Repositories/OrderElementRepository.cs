using OnlineStore.Application.Exceptions;
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

        public async Task DeleteOrderElementByIdAsync(Guid orderElementId)
        {
            OrderElement orderElement = await context.OrderElements.FindAsync(orderElementId);

            if (orderElement == null) throw new NotFoundException();

            context.OrderElements.Remove(orderElement);

            if ((await context.SaveChangesAsync()) < 1) // TODO отлавливать ошибку неудачного обновления
                throw new ApplicationException("Не удалось удалить элемент заказа в БД");
        }
    }
}
