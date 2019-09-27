
using System.Threading.Tasks;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Services.RestaurantService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FoodService.Models;
using FoodService.Services;
using Microsoft.AspNetCore.Identity;
using FoodService.Models.Identity;


namespace FoodService.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var restaurant = await restaurantService.GetRestaurantByIdAsync(id);
            return View(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RestaurantRequest restaurantReq)
        {
            if (!User.IsInRole("Manager"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if(ModelState.IsValid)
            {
                await restaurantService.SaveRestaurantAsync(restaurantReq, User.Identity.Name);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}