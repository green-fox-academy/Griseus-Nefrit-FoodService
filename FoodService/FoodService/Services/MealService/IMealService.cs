using System.Threading.Tasks;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Models;

namespace FoodService.Services.MealService
{
    public interface IMealService
    {
        Task SaveMealAsync(AddMealRequest model);
        Task DeleteMealAsync(long id);
        Task<Meal> GetMealByIdAsync(long MealId);
        Task EditAsync(long id, AddMealRequest addMealRequest);
        Task<AddMealRequest> CreateRequestAsync(long id);
    }
}