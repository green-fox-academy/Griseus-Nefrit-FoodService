using System.Threading.Tasks;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Models;

namespace FoodService.Services.MealService
{
    public interface IMealService
    {
        Task SaveMealAsync(AddMealRequest model);
        Task DeleteMeal(long ID);
        Task<Meal> GetMealByIdAsync(long MealId); 
    }
}