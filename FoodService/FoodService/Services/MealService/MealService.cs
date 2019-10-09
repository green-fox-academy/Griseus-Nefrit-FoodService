using System.Threading.Tasks;
using FoodService.Models.RequestModels.RestaurantRequestModels;
using FoodService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Storage.Blob;
using AutoMapper;
using FoodService.Services.RestaurantService;
using System.Collections.Generic;
using FoodService.Services.BlobService;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace FoodService.Services.MealService
{
    public class MealService : IMealService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IRestaurantService restaurantService;
        private readonly IMapper mapper;
        IBlobStorageService blobStorageService;

        public MealService(ApplicationDbContext applicationDbContext, IMapper mapper, IBlobStorageService blobStorageService, IRestaurantService restaurantService)
        {
            this.applicationDbContext = applicationDbContext;
            this.restaurantService = restaurantService;
            this.mapper = mapper;
            this.blobStorageService = blobStorageService;
        }

        public async Task SaveMealAsync(AddMealRequest addMealRequest)
        {
            var meal = mapper.Map<AddMealRequest, Meal>(addMealRequest);
            meal.Restaurant = await applicationDbContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price)
                .FirstOrDefaultAsync(t => t.RestaurantId == addMealRequest.RestaurantId);
            await applicationDbContext.Meals.AddAsync(meal);
            await applicationDbContext.SaveChangesAsync();
            if (addMealRequest.Image == null)
            {
                meal.ImageUri = "https://dotnetpincerstorage.blob.core.windows.net/mealimages/default/default.png";
            }
            else
            {
                CloudBlockBlob blob = await blobStorageService.MakeBlobFolderAndSaveImageAsync(meal.MealId, addMealRequest.Image);
                await AddImageUriToMealAsync(meal.MealId, blob);
            }
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteMealAsync(long id)
        {
            var meal = await GetMealByIdAsync(id);
            if (meal != null)
            {
                blobStorageService.DeleteBlobFolder(id);
                applicationDbContext.Meals.Remove(meal);
                applicationDbContext.SaveChanges();
            }
        }

        public async Task<Meal> GetMealByIdAsync(long mealId)
        {
            var meal = await applicationDbContext.Meals.Include(m => m.Restaurant).Include(p => p.Price)
                .FirstOrDefaultAsync(m => m.MealId == mealId);
            return meal;
        }

        public async Task<AddMealRequest> CreateMealRequestAsync(long id)
        {
            var meal = await GetMealByIdAsync(id);
            if (meal != null)
            {
                var addMealRequest = mapper.Map<Meal, AddMealRequest>(meal);
                addMealRequest.RestaurantId = meal.Restaurant.RestaurantId;
                return addMealRequest;
            }
            return null;
        }

        public async Task EditAsync(long id, AddMealRequest addMealRequest)
        {
            var meal = await GetMealByIdAsync(id);
            meal = mapper.Map<AddMealRequest, Meal>(addMealRequest, meal);
            meal.Restaurant = await applicationDbContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price)
                .FirstOrDefaultAsync(t => t.RestaurantId == addMealRequest.RestaurantId);
            if (addMealRequest.Image != null)
            {
                CloudBlockBlob blob = await blobStorageService.MakeBlobFolderAndSaveImageAsync(id, addMealRequest.Image);
                await AddImageUriToMealAsync(id, blob);
            }
            await applicationDbContext.SaveChangesAsync();
        }
        
        public async Task<bool> ValidateAccessAsync(long mealId, ClaimsPrincipal user)
        {
            if (user.IsInRole("Admin"))
            {
                return true;
            }
            else
            {
                List<Restaurant> ownedRestaurants = await restaurantService.FindRestaurantByManagerNameOrEmailAsync(user.Identity.Name);
                Meal currentMeal = await GetMealByIdAsync(mealId);
                return ownedRestaurants.Contains(currentMeal.Restaurant);
            }
        }
        
        public async Task AddImageUriToMealAsync(long mealId, CloudBlockBlob blob)
        {
            var meal = await GetMealByIdAsync(mealId);
            meal.ImageUri = blob.SnapshotQualifiedStorageUri.PrimaryUri.ToString();
        }
    }
}