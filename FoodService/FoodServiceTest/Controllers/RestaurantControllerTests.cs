using FoodService.Controllers;
using FoodService.Models.Identity;
using FoodService.Models.RequestModels.RestaurantRequestModels;
using FoodService.Services.OrderService;
using FoodService.Services.RestaurantService;
using FoodService.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FoodServiceTest.Controllers
{
    public class RestaurantControllerTests
    {
        private readonly Mock<IRestaurantService> restaurantService;
        private readonly Mock<IOrderService> orderService;

        public RestaurantControllerTests()
        {
            restaurantService = new Mock<IRestaurantService>();
            orderService = new Mock<IOrderService>();
        }

        [Fact]
        public async Task Add_Restaurant_Should_Call_Service_And_Redirect()
        {
            var restaurantController = new RestaurantController(restaurantService.Object, orderService.Object);
            restaurantController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, "Test username")
                    }, "Test auth type"))
                }
            };

            var restaurantRequest = new RestaurantRequest()
            {
                Name = "Test Restaurant",
            };
            var result = await restaurantController.Add(restaurantRequest);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);

            restaurantService.Verify(s => s.SaveRestaurantAsync(restaurantRequest, "Test username"), Times.Once);
            Assert.Equal(nameof(HomeController.Index), redirectResult.ActionName);
        }
    }
}
