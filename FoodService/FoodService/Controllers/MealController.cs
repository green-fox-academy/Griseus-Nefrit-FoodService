using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Services.MealService;
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
        
        [HttpGet]
        public async Task<IActionResult> Add(long Id)
        {
            AddMealRequest addMealRequest = new AddMealRequest()
            {
                RestaurantId = Id,
                Price = new Price()
            };
            return View(addMealRequest);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(AddMealRequest addMealRequest)
        {
            if (ModelState.IsValid)
            {
                await mealService.SaveMealAsync(addMealRequest);
                return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant", new {id = addMealRequest.RestaurantId});
            }
            return View(addMealRequest);
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
           var requestModel = await mealService.CreateRequest(Id);
           return View(requestModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(AddMealRequest addMealRequest, long Id)
        {
            if (ModelState.IsValid)
            {
                var meal = await mealService.GetMealByIdAsync(Id);
                await mealService.EditAsync(Id, addMealRequest);
                            return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant", new {id = meal.Restaurant.RestaurantId});
            }
            return View(addMealRequest);
        }
    }
}