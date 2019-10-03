using System.Threading.Tasks;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FoodService.Services.RestaurantService;
using System.Collections.Generic;

namespace FoodService.Services.MealService
{
    public class MealService : IMealService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IRestaurantService restaurantService;
        private readonly IMapper iMapper;

        public MealService(ApplicationDbContext applicationDbContext, IRestaurantService restaurantService, IMapper iMapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.restaurantService = restaurantService;
            this.iMapper = iMapper;
        }

        public async Task SaveMealAsync(AddMealRequest addMealRequest)
        {
            var meal = iMapper.Map<AddMealRequest, Meal>(addMealRequest);
            meal.Restaurant = await applicationDbContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price)
                .FirstOrDefaultAsync(t => t.RestaurantId == addMealRequest.RestaurantId);
            await applicationDbContext.Meals.AddAsync(meal);
            await applicationDbContext.SaveChangesAsync();
        }
        
        public async Task DeleteMealAsync(long id)
        {
            var meal = await GetMealByIdAsync(id);
            if (meal != null)
            {
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
            if(meal != null)
            {
                var addMealRequest = iMapper.Map<Meal, AddMealRequest>(meal);
                addMealRequest.RestaurantId = meal.Restaurant.RestaurantId;
                return addMealRequest;
            }
            return null;
        }
        
        public async Task EditAsync(long id, AddMealRequest addMealRequest)
        {
            var meal = await GetMealByIdAsync(id);
            meal = iMapper.Map<AddMealRequest, Meal>(addMealRequest, meal);
            meal.Restaurant = await applicationDbContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price)
                .FirstOrDefaultAsync(t => t.RestaurantId == addMealRequest.RestaurantId);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<bool> ValidateAccessAsync(long mealId, string managerName)
        {
            List<Restaurant> ownedRestaurants = await restaurantService.FindByManagerNameOrEmailAsync(managerName);
            Meal currentMeal = await GetMealByIdAsync(mealId);
            Restaurant restaurantForCurrentMeal = currentMeal.Restaurant;
            return ownedRestaurants.Contains(restaurantForCurrentMeal);
        }
    }
}