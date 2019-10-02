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
using FoodService.Models.ViewModels.Restaurant;

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
        public async Task<IActionResult> Index(SearchRestaurantRequest searchRestaurantRequest, int page = 1)
        {
            List<Restaurant> restaurants;
            if (User.IsInRole("Manager"))
            {
                restaurants = await restaurantService.FindByManagerNameOrEmailAsync(User.Identity.Name);
            }
            else if(String.IsNullOrEmpty(searchRestaurantRequest.FoodName))
            {
                restaurants = await restaurantService.FindAllAsync();
            }
            else
            {
                restaurants = await restaurantService.FindByFoodNameAsync(searchRestaurantRequest.FoodName);
            }

            SearchRestaurantViewModel searchRestaurantViewModel = new SearchRestaurantViewModel()
            {
                PagingList = PagingList.Create(restaurants, 10, page),
                SearchRestaurantRequest = searchRestaurantRequest
            };
            return View(searchRestaurantViewModel);
        }
    }
}

