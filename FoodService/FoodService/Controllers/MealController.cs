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
        public async Task<IActionResult> Add(long id)
        {
            AddMealRequest addMealRequest = new AddMealRequest()
            {
                RestaurantId = id,
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
        public async Task<IActionResult> Delete(long id)
        {
            var meal = await mealService.GetMealByIdAsync(id);
            await mealService.DeleteMealAsync(id);
            return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant", new {id = meal.Restaurant.RestaurantId});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
           var requestModel = await mealService.CreateRequestAsync(id);
            if(requestModel == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home", new { page = 1 });
            }
           return View(requestModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(AddMealRequest addMealRequest, long id)
        {
            if (ModelState.IsValid)
            {
                var meal = await mealService.GetMealByIdAsync(id);
                await mealService.EditAsync(id, addMealRequest);
                            return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant", new {id = meal.Restaurant.RestaurantId});
            }
            return View(addMealRequest);
        }
    }
}