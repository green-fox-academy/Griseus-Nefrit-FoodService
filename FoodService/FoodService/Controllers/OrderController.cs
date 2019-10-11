using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.Identity;
using FoodService.Models.RequestModels.OrderRequestModels;
using FoodService.Models.ViewModels.OrderViewModels;
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
        private readonly IRestaurantService restaurantService;

        public OrderController(IOrderService orderService, IMealService mealService, IRestaurantService restaurantService)
        {
            this.orderService = orderService;
            this.mealService = mealService;
            this.restaurantService = restaurantService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(long id)
        {
            var shoppingCart = await orderService.AddMealToOrderAsync(id, User.Identity.Name);
            var meal = await mealService.GetMealByIdAsync(id);
            return RedirectToAction(nameof(RestaurantController.Index), "Restaurant", new { id = meal.Restaurant.RestaurantId });
        }

        [HttpGet]
        public async Task<IActionResult> Submit(long id)
        {
            var shoppingCart = await orderService.CreateShoppingCartRequestByUserAndRestaurantAsync(User.Identity.Name, null, id);
            return View(shoppingCart);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(long id, Address address)
        {
            if (ModelState.IsValid)
            {
                await orderService.SaveOrderAsync(id, address);
                return RedirectToAction(nameof(OrderController.ThankYou), "Order");
            }
            var shoppingCartRequest = await orderService.CreateShoppingCartRequestByIdAsync(address, id);
            return View(shoppingCartRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            if (!await orderService.ValidateAccessAsync(id, User.Identity.Name))
            {
                return RedirectToAction(nameof(OrderController.Submit), "Order");
            }

            var cartItem = await orderService.GetCartItemByIdAsync(id);
            await orderService.DeleteCartItemAsync(id);

            return RedirectToAction(nameof(OrderController.Submit), "Order", new { id = cartItem.Order.Restaurant.RestaurantId});
        }

        [HttpGet]
        public IActionResult ThankYou()
        {
            return View();
        }
        
        [Authorize(Roles = "Manager, Admin")]
        [HttpGet]
        public async Task<IActionResult> CurrentOrder()
        {
            var user = User;
            var orderList = await orderService.GetOrdersByManagerAsync(user);
            return View(new CurrentOrderViewModel
            {
                RestaurantsOfManager = await restaurantService.GetRestaurantsByManagerAsync(user),
                Orders = orderList
            });
        }
    }
}