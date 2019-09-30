
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
using FoodService.Models.ViewModels;
using FoodService.Models.ViewModels.Restaurant;

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
        public async Task<IActionResult> Add(RestaurantRequest restaurantRequest)
        {
            if (!User.IsInRole("Manager"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if(ModelState.IsValid)
            {
                await restaurantService.SaveRestaurantAsync(restaurantRequest, User.Identity.Name);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            EditRestaurantViewModel editRestaurantViewModel = new EditRestaurantViewModel()
            {
                RestaurantRequest = restaurantRequest,
                Meals = null,
                RestaurantId = 0
            };
            return View(editRestaurantViewModel);
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

            EditRestaurantViewModel editRestaurantViewModel = await restaurantService.BuildEditRestaurantViewModel(id);
            return View(editRestaurantViewModel);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditRestaurantViewModel editRestaurantViewModel, long id)
        {
            if (!await restaurantService.ValidateAccess(id, User.Identity.Name))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (ModelState.IsValid)
            {
                await restaurantService.EditRestaurantAsync(id, editRestaurantViewModel.RestaurantRequest);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            EditRestaurantViewModel editRestaurantViewModel1 = await restaurantService.BuildEditRestaurantViewModel(id, editRestaurantViewModel.RestaurantRequest);
            return View(editRestaurantViewModel1);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(long id)
        {
            var restaurant = await restaurantService.GetRestaurantByIdAsync(id);
            return View(restaurant);
        }
    }
}