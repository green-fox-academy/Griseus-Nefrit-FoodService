using System.Threading.Tasks;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Models;

namespace FoodService.Services.MealService
{
    public interface IMealService
    {
        Task<long> SaveMealAsync(AddMealRequest model);
        Task DeleteMealAsync(long id);
        Task<Meal> GetMealByIdAsync(long mealId);
        Task EditAsync(long id, AddMealRequest addMealRequest);
        Task<AddMealRequest> CreateRequestAsync(long id);
        Task addURIToAMealAsync(long mealID, Microsoft.Azure.Storage.Blob.CloudBlockBlob blob);
    }
}