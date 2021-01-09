using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.API.Models;
using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Application.Services.Interfaces;
using OnlineStore.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using OnlineStore.API.Identity.Models;
using Microsoft.AspNetCore.Identity;
using OnlineStore.API.Identity;
using System.Security.Claims;

namespace OnlineStore.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderService orderService;
        private readonly IMapper mapper;
        public OrderController(UserManager<User> userManager,
            IOrderRepository orderRepository,
            IOrderService orderService,
            IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.orderService = orderService;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        //[Authorize(Role="Manager")]
        [HttpGet]
        [Route("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderDto>))]
        public async Task<IActionResult> GetAllOrders([FromQuery] OrderGetAllPagedInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var orders = await orderService.GetAllOrdersWithPagingAsync(model.PageNumber, model.PageSize);

            return Ok(orders);
        }

        [Authorize(Roles = Roles.User)]
        [HttpPost]
        [Route("add")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderDto))]
        public async Task<IActionResult> AddOrder([FromBody] OrderAddInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //Получаем userId из контекста
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var productsAndCountsList = new List<ProductAndCountDto>();

            foreach(var pAndCtsInput in model.ProductsIdAndCountsList)
            {
                productsAndCountsList.Add(new ProductAndCountDto()
                {
                    ProductId = pAndCtsInput.ProductId,
                    ProductCount = pAndCtsInput.ProductCount
                });
            }

            var listOfProductsAndCounts = new ListOfProductsAndCountsDto()
            {
                ProductsIdAndCountsList = productsAndCountsList
            };

            var addedOrder = await orderService.AddOrderAsync(listOfProductsAndCounts, userId);

            return Ok(addedOrder);
        }

        //[Authorize(Role="Manager")]
        [HttpPost]
        [Route("confirm-order/{orderId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmOrder(string orderId,
            [FromBody] OrderConfirmInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Guid orderIdGuid = Guid.Parse(orderId);

            await orderService.ConfirmOrderAsync(orderIdGuid, model.ShipmentDate);

            return Ok();
        }

        //[Authorize(Role="Manager")]
        [HttpPost]
        [Route("close-order/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CloseOrder(string orderId)
        {
            Guid orderIdGuid = Guid.Parse(orderId);

            await orderService.CloseOrderAsync(orderIdGuid);

            return Ok();
        }

        //[Authorize(Role="User")]
        [HttpPost]
        [Route("delete-order/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteOrder(string orderId)
        {
            Guid orderIdGuid = Guid.Parse(orderId);

            await orderService.DeleteOrderAsync(orderIdGuid);

            return Ok();
        }
    }
}
