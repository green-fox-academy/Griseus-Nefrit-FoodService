using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Services;
using FoodService.Services.MealService;
using FoodService.Services.MealService;
using FoodService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Add(AddMealRequest addMealRequest)
        {
            await mealService.SaveMealAsync(addMealRequest);
            
            return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant", new {id = addMealRequest.RestaurantId});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long Id)
        {
            var meal = await mealService.GetMealByIdAsync(Id);
            await mealService.DeleteMeal(Id);
            return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant", new {id = meal.Restaurant.RestaurantId});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long Id)
        {
           var viewmodel = await mealService.CreateViewModel(Id);
           return View(viewmodel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(AddMealRequest addMealRequest, long Id)
        {
           var meal = await mealService.GetMealByIdAsync(Id);
          if (ModelState.IsValid)
            {
                await mealService.EditAsync(Id, addMealRequest);
            }
            return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant", new {id = meal.Restaurant.RestaurantId});
        }
    }
}