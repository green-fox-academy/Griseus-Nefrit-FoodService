using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Services;
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
            var restaurants = await restaurantService.findAll();
            var model = PagingList.Create(restaurants, 10, page);
            return View(model);
        }
    }
}