using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<Order> AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderByIdAsync(Guid orderId);
        Task<int> GetNewOrderNumberAsync();
        Task<IList<Order>> GetAllOrdersWithPagingAsync(int pageNumber, int pageSize);
    }
}
