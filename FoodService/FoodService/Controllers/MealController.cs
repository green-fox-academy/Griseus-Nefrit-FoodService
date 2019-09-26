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
        public async Task<IActionResult> Add(AddMealRequest addMealRequest)
        {
            await mealService.SaveMealAsync(addMealRequest);
            return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant", new {id = 1});
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
           // var meal = await mealService.GetMealByIdAsync(Id);
           var viewmodel = new EditRestaurantViewModel();
           var meal = await mealService.GetMealByIdAsync(Id);
           
           viewmodel.AddMealRequest= new AddMealRequest();
           viewmodel.AddMealRequest.Price = new Price();
           viewmodel.Restaurant = meal.Restaurant;
           viewmodel.AddMealRequest.Name = meal.Name;
           viewmodel.AddMealRequest.Description = meal.Description;
           viewmodel.AddMealRequest.Price.Amount = meal.Price.Amount;
           viewmodel.AddMealRequest.Price.Currency = meal.Price.Currency;
           return View(viewmodel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(AddMealRequest addMealRequest)
        {
            await mealService.SaveMealAsync(addMealRequest);
            return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant", new {id = 1});
        }
        
    }
}