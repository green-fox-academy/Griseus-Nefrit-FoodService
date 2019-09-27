using System.Threading.Tasks;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Services.RestaurantService;
using Microsoft.AspNetCore.Mvc;

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
    }
}