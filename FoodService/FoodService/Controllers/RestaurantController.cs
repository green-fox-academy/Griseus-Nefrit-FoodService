using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Services;
using FoodService.ViewModels;
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
        
        [HttpGet("")]
        public async Task<IActionResult> EditRestaurant([FromRoute] long id)
        {
            var viewmodel = new EditRestaurantViewModel();
            var restaurant = await restaurantService.GetRestaurantByIdAsync(id);
            var meal = new Meal();
            var model = new AddMealRequest();
            viewmodel.Restaurant = restaurant;
            viewmodel.Meal = meal;
            viewmodel.AddMealRequest = model;
            return View(viewmodel);
        }

        [HttpPost("/add")]
        public async Task<IActionResult> AddMeal([FromRoute] long id, AddMealRequest model)
        {
            await restaurantService.SaveMealAsync(model, id);
            return RedirectToAction(nameof(EditRestaurant));
        }

        [HttpPost("/delete")]
        public async Task<IActionResult> DeleteMeal(long MealId)
        {
            var meal = await restaurantService.GetMealByIdAsync(MealId);
            await restaurantService.DeleteMeal(meal);

            return RedirectToAction(nameof(EditRestaurant));
        }
    }
}