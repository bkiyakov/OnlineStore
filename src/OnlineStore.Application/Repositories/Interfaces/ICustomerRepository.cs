using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByIdAsync(Guid customerId);
        Task<Customer> GetCustomerByCodeAsync(string customerCode);
        Task<Customer> GetCustomerByUserIdAsync(string userId);
        Task<Customer> AddCustomerAsync(Customer newCustomer);
        Task DeleteCustomerByIdAsync(Guid customerId);
        Task UpdateCustomerAsync(Customer customer);
    }
}
