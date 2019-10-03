
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Services.BlobService;
using FoodService.Services.MealService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage.Blob;


namespace FoodService.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealService mealService;
        IBlobStorageService blobStorageService;

        public MealController(IMealService mealService, IBlobStorageService blobStorageService)
        {
            this.mealService = mealService;
            this.blobStorageService = blobStorageService;
        }

        [HttpGet]
        public IActionResult Add(long id)
        {
            AddMealRequest addMealRequest = new AddMealRequest()
            {
                RestaurantId = id,
                Price = new Price()
            };
            return View(addMealRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMealRequest addMealRequest, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                long mealID = await mealService.SaveMealAsync(addMealRequest);
                CloudBlockBlob blob = await blobStorageService.makeBlobFolderAndSaveImageAsync(mealID, image);
                await mealService.addURIToAMealAsync(mealID, blob);


                return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant", new { id = addMealRequest.RestaurantId });
            }
            return View(addMealRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var meal = await mealService.GetMealByIdAsync(id);
            blobStorageService.deleteBlobFolder(id);

            await mealService.DeleteMealAsync(id);
            return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant", new { id = meal.Restaurant.RestaurantId });
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
        public async Task<IActionResult> Edit(AddMealRequest addMealRequest, long id, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var meal = await mealService.GetMealByIdAsync(id);
                if (image != null)
                {
                    CloudBlockBlob blob = await blobStorageService.makeBlobFolderAndSaveImageAsync(id, image);
                    await mealService.addURIToAMealAsync(id, blob);
                }

                if(meal != null)
                {
                    await mealService.EditAsync(id, addMealRequest);
                }

                return RedirectToAction(nameof(RestaurantController.Edit), "Restaurant", new { id = meal.Restaurant.RestaurantId });
            }
            return View(addMealRequest);
        }
    }
}


