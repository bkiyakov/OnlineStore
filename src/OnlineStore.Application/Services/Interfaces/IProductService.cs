using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<decimal> GetProductPriceByIdAsync(Guid productId);
    }
}
