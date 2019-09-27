using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize(Roles = "Manager")]
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

            var restaurant = await restaurantService.FindByIdAsync(id);
            var request = new RestaurantRequest
            {
                Name = restaurant.Name,
                Description = restaurant.Description,
                City = restaurant.City,
                FoodType = restaurant.FoodType,
                PriceCategory = restaurant.PriceCategory
            };
            return View(request);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(RestaurantRequest restaurantReq, long id)
        {
            if (!await restaurantService.ValidateAccess(id, User.Identity.Name))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (ModelState.IsValid)
            {
                await restaurantService.EditRestaurantAsync(id, restaurantReq);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(restaurantReq);
        }
    }
}