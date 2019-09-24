using System.Threading.Tasks;
using FoodService.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodService.Controllers
{
    [Route("partner/restaurants/edit/{id}")]
    public class RestaurantController : Controller
    {
        private readonly IMealService mealService;
        private readonly IRestaurantService restaurantService;

        public RestaurantController(IMealService mealService, IRestaurantService restaurantService)
        {
            this.mealService = mealService;
            this.restaurantService = restaurantService;
        }
        
        [HttpGet]
        public async Task<IActionResult> EditRestaurant([FromRoute] long id)
        {
            var restaurant = await restaurantService.getRestaurantByIdAsync(id); 
            return View(restaurant);
        }
        
        
    }
}