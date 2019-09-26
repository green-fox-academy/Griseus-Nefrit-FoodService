using System.Threading.Tasks;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Services.MealService
{
    public class MealService : IMealService
    {
        private readonly ApplicationContext applicationContext;

        public MealService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
        
        public async Task SaveMealAsync(AddMealRequest model)
        {
            var meal = new Meal();
            meal.Description = model.Description;
            meal.Price.Amount = model.Price.Amount;
            meal.Price.Currency = model.Price.Currency;
            meal.Name = model.Name;
         //   meal.Restaurant.RestaurantId = model.RestaurantId;
            await applicationContext.Meals.AddAsync(meal);
            await applicationContext.SaveChangesAsync();
        }
        
        public async Task DeleteMeal(long ID)
        {
            var meal = await GetMealByIdAsync(ID);
            if (meal != null)
            {
                applicationContext.Meals.Remove(meal);
                applicationContext.SaveChanges();
            }
        }
        
        public async Task<Meal> GetMealByIdAsync(long MealId)
        {
            var meal = await applicationContext.Meals.FirstOrDefaultAsync(m => m.MealId == MealId);
            if (meal == null)
            {
                return null;
            }
            return meal;
        }
    }
}