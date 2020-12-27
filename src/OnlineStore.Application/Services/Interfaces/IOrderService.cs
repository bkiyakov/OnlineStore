using OnlineStore.Application.Dtos;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> AddOrderAsync(ListOfProductsAndCountsDto productsAndCountsList, int userId);

        Task ConfirmOrder(Guid orderId, DateTime shipmentDate);
    }
}
