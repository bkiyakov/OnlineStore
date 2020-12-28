using OnlineStore.Application.Dtos;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interfaces
{
    public interface IOrderElementService
    {
        Task<OrderElementDto> AddOrderElementAsync(OrderDto order, Guid customerId,
            Guid productId, int productCount);
    }
}
