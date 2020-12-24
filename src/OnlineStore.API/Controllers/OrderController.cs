using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Application.Services.Interfaces;
using OnlineStore.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OnlineStore.Application.ViewModels.OrderViewModel;

namespace OnlineStore.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderService orderService;
        public OrderController(IOrderRepository orderRepository, IOrderService orderService)
        {
            this.orderRepository = orderRepository;
            this.orderService = orderService;
        }

        //[Authorize(Role="Manager")]
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await orderRepository.GetAllOrdersAsync();

            return Ok(orders);
        }

        //[Authorize(Role="User")]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddOrder([FromBody] OrderViewModel model)
        {
            //Получаем userId из контекста
            var userId = 1; // TODO поменять на реальный

            var addedOrder = await orderService.AddOrderAsync(model, userId);

            return Ok(addedOrder);
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            var orderNiewModel = new OrderViewModel
            {
                ProductsIdAndCountsList = new List<ProductAndCount>
                {
                    new ProductAndCount
                    {
                        ProductId = Guid.NewGuid().ToString(),
                        ProductCount = 2
                    }
                }
            };

            return Ok(orderNiewModel);
        }
    }
}
