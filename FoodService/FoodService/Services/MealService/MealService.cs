using System.Threading.Tasks;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Services.MealService
{
    public class MealService : IMealService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public MealService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        
        public async Task SaveMealAsync(AddMealRequest model)
        {
            var meal = new Meal();
            meal.Price = new Price();
            meal.Restaurant = await applicationDbContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price)
                .FirstOrDefaultAsync(t => t.RestaurantId == model.RestaurantId);
            meal.Description = model.Description;
            meal.Price.Amount = model.Price.Amount;
            meal.Price.Currency = model.Price.Currency;
            meal.Name = model.Name;
            meal.Restaurant.RestaurantId = model.RestaurantId;
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
            if (meal == null)
            {
                return null;
            }
            return meal;
        }

        public async Task<AddMealRequest> CreateRequestAsync(long id)
        {
            var meal = await GetMealByIdAsync(id);
            AddMealRequest addMealRequest = new AddMealRequest()
            {
                Name = meal.Name,
                Description = meal.Description,
                Price = new Price()
                {
                    Amount = meal.Price.Amount,
                    Currency = meal.Price.Currency,
                },
                RestaurantId = meal.Restaurant.RestaurantId
            };
            return addMealRequest;
        }
        
        public async Task EditAsync(long id, AddMealRequest addMealRequest)
        {
            var meal = await GetMealByIdAsync(id);
            meal.Price = new Price();
            meal.Restaurant = await applicationDbContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price)
                .FirstOrDefaultAsync(t => t.RestaurantId == addMealRequest.RestaurantId);
            meal.Description = addMealRequest.Description;
            meal.Price.Amount = addMealRequest.Price.Amount;
            meal.Price.Currency = addMealRequest.Price.Currency;
            meal.Name = addMealRequest.Name;
            meal.Restaurant.RestaurantId = addMealRequest.RestaurantId;
            await applicationDbContext.SaveChangesAsync();
        }
    }
}