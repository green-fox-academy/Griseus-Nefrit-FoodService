using System.Collections.Generic;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;

namespace FoodService.Services
{
    public interface IRestaurantService
    {
        Task<Restaurant> GetRestaurantByIdAsync(long id);
        Task<Meal> GetMealByIdAsync(long MealId);
        Task DeleteMeal(Meal meal);
        Task SaveMealAsync(AddMealRequest model, long id);
    }
}