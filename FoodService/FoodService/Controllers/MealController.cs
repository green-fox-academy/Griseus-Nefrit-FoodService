using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Services.BlobService;
using FoodService.Models.RequestModels.RestaurantRequestModels;
using FoodService.Services.MealService;
using FoodService.Services.RestaurantService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage.Blob;

namespace FoodService.Controllers
{
    [Authorize]
    public class MealController : Controller
    {
        private readonly IMealService mealService;
        private readonly IRestaurantService restaurantService;

        public MealController(IMealService mealService, IRestaurantService restaurantService)
        {
            this.mealService = mealService;
            this.restaurantService = restaurantService;
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpGet]
        public async Task<IActionResult> Add(long id)
        {
            if (!await restaurantService.ValidateAccessAsync(id, User))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            AddMealRequest addMealRequest = new AddMealRequest()
            {
                RestaurantId = id,
                Price = new Price()
            };
            return View(addMealRequest);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(AddMealRequest addMealRequest)
        {
            if (!await restaurantService.ValidateAccessAsync(addMealRequest.RestaurantId, User))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (ModelState.IsValid)
            {
                await mealService.SaveMealAsync(addMealRequest);
                return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant", new { id = addMealRequest.RestaurantId });
            }
            return View(addMealRequest);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            if (!await mealService.ValidateAccessAsync(id, User))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var meal = await mealService.GetMealByIdAsync(id);
            await mealService.DeleteMealAsync(id);
            return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant", new { id = meal.Restaurant.RestaurantId });
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            if (!await mealService.ValidateAccessAsync(id, User))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var requestModel = await mealService.CreateMealRequestAsync(id);
            if (requestModel == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home", new { page = 1 });
            }
            return View(requestModel);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(AddMealRequest addMealRequest, long id)
        {
            if (!await mealService.ValidateAccessAsync(id, User))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (ModelState.IsValid)
            {
                var meal = await mealService.GetMealByIdAsync(id);
                if (meal != null)
                {
                    await mealService.EditAsync(id, addMealRequest);
                }
                return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant", new { id = meal.Restaurant.RestaurantId });
            }
            return View(addMealRequest);
        }
    }
}


