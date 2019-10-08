using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models.Identity;
using FoodService.Services.MealService;
using FoodService.Services.OrderService;
using FoodService.Services.RestaurantService;
using FoodService.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodService.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IMealService mealService;

        public OrderController(IOrderService orderService, IMealService mealService)
        {
            this.orderService = orderService;
            this.mealService = mealService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(long id)
        {
            //var user = User;
            var shoppingCart = await orderService.AddMealToOrderAsync(id, User.Identity.Name);
            var meal = await mealService.GetMealByIdAsync(id);
            return RedirectToAction(nameof(RestaurantController.Index), "Restaurant", new { id = meal.Restaurant.RestaurantId });
        }
    }
}