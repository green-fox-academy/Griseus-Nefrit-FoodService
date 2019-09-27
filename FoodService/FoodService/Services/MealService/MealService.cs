using System.Threading.Tasks;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Services.MealService
{
    public class MealService : IMealService
    {
        private readonly ApplicationDBContext _applicationDbContext;

        public MealService(ApplicationDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public async Task SaveMealAsync(AddMealRequest model)
        {
            var meal = new Meal();
            meal.Price = new Price();
            meal.Restaurant = await _applicationDbContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price)
                .FirstOrDefaultAsync(t => t.RestaurantId == model.RestaurantId);
            meal.Description = model.Description;
            meal.Price.Amount = model.Price.Amount;
            meal.Price.Currency = model.Price.Currency;
            meal.Name = model.Name;
            meal.Restaurant.RestaurantId = model.RestaurantId;
            await _applicationDbContext.Meals.AddAsync(meal);
            await _applicationDbContext.SaveChangesAsync();
        }
        
        public async Task DeleteMeal(long ID)
        {
            var meal = await GetMealByIdAsync(ID);
            if (meal != null)
            {
                _applicationDbContext.Meals.Remove(meal);
                _applicationDbContext.SaveChanges();
            }
        }
        
        public async Task<Meal> GetMealByIdAsync(long MealId)
        {
            var meal = await _applicationDbContext.Meals.Include(m => m.Restaurant).Include(p => p.Price).FirstOrDefaultAsync(m => m.MealId == MealId);
            if (meal == null)
            {
                return null;
            }
            return meal;
        }

        public async Task<AddMealRequest> CreateRequest(long Id)
        {
            var meal = await GetMealByIdAsync(Id);
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
        
        public async Task EditAsync(long Id, AddMealRequest addMealRequest)
        {
            var meal = await GetMealByIdAsync(Id);
            meal.Price = new Price();
            meal.Restaurant = await _applicationDbContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price)
                .FirstOrDefaultAsync(t => t.RestaurantId == addMealRequest.RestaurantId);
            meal.Description = addMealRequest.Description;
            meal.Price.Amount = addMealRequest.Price.Amount;
            meal.Price.Currency = addMealRequest.Price.Currency;
            meal.Name = addMealRequest.Name;
            meal.Restaurant.RestaurantId = addMealRequest.RestaurantId;
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}