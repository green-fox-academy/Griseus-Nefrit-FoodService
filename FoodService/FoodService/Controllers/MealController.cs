using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Services;
using FoodService.Services.MealService;
using FoodService.Services.MealService;
using FoodService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FoodService.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealService mealService;

        public MealController(IMealService mealService)
        {
            this.mealService = mealService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMealRequest model)
        {
            await mealService.SaveMealAsync(model);
            return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long Id)
        {
          //  var meal = await mealService.GetMealByIdAsync(Id);
            await mealService.DeleteMeal(Id);
            return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant");
        }
    }
}