using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationContext applicationContext;

        public RestaurantService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task<Restaurant> GetRestaurantByIdAsync(long id)
        {
            var restaurant = await applicationContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.MealPrice).FirstOrDefaultAsync(t => t.RestaurantId == id);
            if (restaurant == null)
            {
                return null;
            }
            return restaurant;
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

        public async Task SaveMealAsync(AddMealRequest model, long id)
        {
            var meal = new Meal();
            meal.Description = model.Description;
          //  meal.MealPrice.Amount = model.Amount;
            meal.Name = model.Name;
            await applicationContext.Meals.AddAsync(meal);
            await applicationContext.SaveChangesAsync();
        }
        
        public async Task DeleteMeal(Meal meal)
        {
            applicationContext.Meals.Remove(meal);
            applicationContext.SaveChanges();
        }
    }
}