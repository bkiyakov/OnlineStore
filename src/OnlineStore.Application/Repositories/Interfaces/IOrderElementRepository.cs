using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Repositories.Interfaces
{
    public interface IOrderElementRepository
    {
        Task<OrderElement> AddOrdeElementAsync(OrderElement newOrderElement);
    }
}
