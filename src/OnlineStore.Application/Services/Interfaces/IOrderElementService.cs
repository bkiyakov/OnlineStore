using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interfaces
{
    public interface IOrderElementService
    {
        Task<OrderElement> AddOrderElementAsync(Order order, Guid productId, int productCount);
    }
}
