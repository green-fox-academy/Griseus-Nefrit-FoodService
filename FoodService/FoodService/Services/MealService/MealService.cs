using System.Threading.Tasks;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace FoodService.Services.MealService
{
    public class MealService : IMealService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper iMapper;

        public MealService(ApplicationDbContext applicationDbContext, IMapper iMapper)
        {
            this.applicationDbContext = applicationDbContext;
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
            var meal = await applicationDbContext.Meals.Include(m => m.Restaurant).Include(p => p.Price).FirstOrDefaultAsync(m => m.MealId == mealId);
            //return meal ?? null;
            if (meal == null)
            {
                System.Console.WriteLine("null");
                return null;
            }
            System.Console.WriteLine("nem null");
            return meal;
        }

        public async Task<AddMealRequest> CreateRequestAsync(long id)
        {
            var meal = await GetMealByIdAsync(id);
            if(meal == null)
            {
                return null;
            }
            var addMealRequest = iMapper.Map<Meal, AddMealRequest>(meal);
            addMealRequest.RestaurantId = meal.Restaurant.RestaurantId;
            return addMealRequest;
        }
        
        public async Task EditAsync(long id, AddMealRequest addMealRequest)
        {
            var meal = await GetMealByIdAsync(id);
            meal = iMapper.Map<AddMealRequest, Meal>(addMealRequest, meal);
            meal.Restaurant = await applicationDbContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price)
                .FirstOrDefaultAsync(t => t.RestaurantId == addMealRequest.RestaurantId);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}