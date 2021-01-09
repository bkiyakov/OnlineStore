using OnlineStore.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> AddCustomerAsync(CustomerDto customerDto);
        Task<Guid> GetCustomerIdByUserIdAsync(string userId);
    }
}
