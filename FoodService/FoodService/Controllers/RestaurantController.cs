
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
using Microsoft.AspNetCore.Authorization;


namespace FoodService.Controllers
{
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
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

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            if (!await restaurantService.ValidateAccess(id, User.Identity.Name))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var restaurant = await restaurantService.GetRestaurantByIdAsync(id);
            /*/var request = new RestaurantRequest
            {
                Name = restaurant.Name,
                Description = restaurant.Description,
                City = restaurant.City,
                FoodType = restaurant.FoodType,
                PriceCategory = restaurant.PriceCategory
            };*/
            return View(restaurant);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Restaurant restaurant, long id)
        {
            if (!await restaurantService.ValidateAccess(id, User.Identity.Name))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (ModelState.IsValid)
            {
                await restaurantService.EditRestaurantAsync(id, restaurant);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(restaurant);
        }
    }
}