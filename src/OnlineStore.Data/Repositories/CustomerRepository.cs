using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Exceptions;
using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly StoreDbContext context;
        public CustomerRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
        {
            return await context.Customers.FindAsync(customerId);
        }

        public async Task<Customer> AddCustomerAsync(Customer newCustomer)
        {
            var addedCustomer = context.Customers.Add(newCustomer);

            await context.SaveChangesAsync();

            return addedCustomer.Entity;
        }

        public async Task DeleteCustomerByIdAsync(Guid customerId)
        {
            Customer customer = await context.Customers.FindAsync(customerId);

            if (customer == null) throw new NotFoundException();

            context.Customers.Remove(customer);

            if ((await context.SaveChangesAsync()) < 1) // TODO отлавливать ошибку неудачного обновления
                throw new ApplicationException("Не удалось удалить заказчика в БД");
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            Customer customerFromDb = await context.Customers.FindAsync(customer.Id);

            if (customerFromDb == null) throw new NotFoundException();

            customerFromDb.Code = customer.Code;
            customerFromDb.Name = customer.Name;
            customerFromDb.Address = customer.Address;
            customerFromDb.Discount = customer.Discount;

            if ((await context.SaveChangesAsync()) < 1) // TODO отлавливать ошибку неудачного обновления
                throw new ApplicationException("Не удалось обновить заказчика в БД");
        }

        public async Task<Customer> GetCustomerByCodeAsync(string customerCode)
        {
            return await context.Customers.FirstOrDefaultAsync(c => c.Code == customerCode);
        }

        public async Task<Customer> GetCustomerByUserIdAsync(string userId)
        {
            return await context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
