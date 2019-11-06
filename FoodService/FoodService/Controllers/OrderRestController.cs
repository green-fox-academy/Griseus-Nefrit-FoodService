using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models.Api;
using FoodService.Services.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodService.Controllers
{
    [Authorize]
    [Route("api/orders")]
    [ApiController]
    public class OrderRestController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderRestController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        // POST: api/orders
        [HttpPost]
        public async Task PostOrder(OrderApi postedOrder)
        {
            await orderService.SavePostedOrderApiAsync(postedOrder);
        }
    }
}