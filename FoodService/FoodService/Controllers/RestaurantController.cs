using FoodService.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodService.Controllers
{
    [Route("partner/restaurants")]
    public class RestaurantController : Controller
    {
        private readonly IMealService mealService;
        private readonly IRestaurantService restaurantService;

        public RestaurantController(IMealService mealService, IRestaurantService restaurantService)
        {
            this.mealService = mealService;
            this.restaurantService = restaurantService;
        }
        
        [HttpGet("{id}/edit")]
        public IActionResult EditRestaurant([FromRoute] long id)
        {
            var restaurant = restaurantService.getRestaurantById(id); 
            return View(restaurant);
        }
        
        
    }
}