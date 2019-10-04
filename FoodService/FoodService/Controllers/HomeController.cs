using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.RestaurantRequestModels;
using FoodService.Models.ViewModels.RestaurantViewModels;
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
        public async Task<IActionResult> Index(SearchRestaurantRequest searchRestaurantRequest, int page = 1)
        {
            var user = User;
            var restaurants = await restaurantService.GetRestaurantsByRequestAsync(page, user, searchRestaurantRequest);
            
            return View(new SearchRestaurantViewModel
            {
                UniqueCities = await restaurantService.GetUniqueCities(),
                PagingList = restaurants,
                SearchRestaurantRequest = searchRestaurantRequest
            });
        }
    }
}

