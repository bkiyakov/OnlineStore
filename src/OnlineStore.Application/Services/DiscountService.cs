using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly ICustomerRepository customerRepository;
        public DiscountService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public async Task<int> GetCustomerDiscountPercentAsync(Guid customerId)
        {
            var customer = await customerRepository.GetCustomerByIdAsync(customerId);
            
            var customerDiscount = customer.Discount;

            return customerDiscount;
        }
    }
}
