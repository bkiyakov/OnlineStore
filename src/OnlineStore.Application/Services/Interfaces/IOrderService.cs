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
        Task<OrderDto> AddOrderAsync(ListOfProductsAndCountsDto productsAndCountsList, string userId);

        Task ConfirmOrderAsync(Guid orderId, DateTime shipmentDate);
        Task CloseOrderAsync(Guid orderId);
        Task DeleteOrderAsync(Guid orderId);
        Task<OrderListPagingDto> GetAllOrdersWithPagingAsync(int pageNumber, int pageSize);
    }
}
