using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Services;
using FoodService.Services.RestaurantService;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace FoodService.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRestaurantService restaurantService;

        public HomeController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index(int page = 1)
        {
            List<Restaurant> restaurants;
            if (User.IsInRole("Manager"))
            {
                restaurants = await restaurantService.FindByManagerNameOrEmailAsync(User.Identity.Name);
            }
            else/* if(searchedFoodName == "" || searchedFoodName == null)*/
            {
                restaurants = await restaurantService.FindAllAsync();
            }
            /*else
            {
                restaurants = await restaurantService.FindByFoodNameAsync(searchedFoodName);
            }*/
            var model = PagingList.Create(restaurants, 10, page);
            return View(model);
        }

        [HttpPost]
        public IActionResult SearchRestaurantAsync(string searchedFoodName)
        {          
            return RedirectToAction(nameof(HomeController.Index), "Home", searchedFoodName);
        }
    }
}

