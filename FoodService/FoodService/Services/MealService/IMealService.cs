using System.Threading.Tasks;
using FoodService.Models.RequestModels.RestaurantRequestModels;
using FoodService.Models;
using Microsoft.AspNetCore.Http;

namespace FoodService.Services.MealService
{
    public interface IMealService
    {
        Task SaveMealAsync(AddMealRequest model);
        Task DeleteMealAsync(long id);
        Task<Meal> GetMealByIdAsync(long mealId);
        Task EditAsync(long id, AddMealRequest addMealRequest);
        Task<AddMealRequest> CreateMealRequestAsync(long id);
        Task<bool> ValidateAccessAsync(long mealId, string managerName);
        Task<AddMealRequest> CreateRequestAsync(long id);
        Task AddImageUriToMealAsync(long mealID, Microsoft.Azure.Storage.Blob.CloudBlockBlob blob);
    }
}