﻿using Microsoft.AspNetCore.Http;
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

namespace OnlineStore.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderService orderService;
        private readonly IMapper mapper;
        public OrderController(IOrderRepository orderRepository,
            IOrderService orderService,
            IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.orderService = orderService;
            this.mapper = mapper;
        }

        //[Authorize(Role="Manager")]
        [HttpGet]
        [Route("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderDto>))]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await orderRepository.GetAllOrdersAsync();

            var result = mapper.Map<IEnumerable<OrderDto>>(orders);

            return Ok(result);
        }

        //[Authorize(Role="User")]
        [HttpPost]
        [Route("add")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderDto))]
        public async Task<IActionResult> AddOrder([FromBody] OrderAddInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //Получаем userId из контекста
            var userId = 1; // TODO поменять на реальный

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

        [HttpPost]
        [Route("confirm-order/{orderId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmOrder(string orderId,
            [FromBody] OrderConfirmInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Guid orderIdGuid = Guid.Parse(orderId);

            await orderService.ConfirmOrder(orderIdGuid, model.ShipmentDate);

            return Ok();
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            var orderViewModel = new ListOfProductsAndCountsDto
            {
                ProductsIdAndCountsList = new List<ProductAndCountDto>
                {
                    new ProductAndCountDto
                    {
                        ProductId = Guid.NewGuid().ToString(),
                        ProductCount = 2
                    }
                }
            };

            return Ok(orderViewModel);
        }
    }
}
