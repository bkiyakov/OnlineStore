using OnlineStore.Application.ViewModels;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> AddOrderAsync(OrderViewModel orderViewModel, int userId);
    }
}
