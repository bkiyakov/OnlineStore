using OnlineStore.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class DiscountService : IDiscountService
    {
        public async Task<int> GetCustomerDiscountPercentAsync(Guid customerId)
        {
            // TODO реализовать
            await Task.Delay(100);
            return 20;
        }
    }
}
