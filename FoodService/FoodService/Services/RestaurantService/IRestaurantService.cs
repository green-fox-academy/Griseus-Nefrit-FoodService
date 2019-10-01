using System.Collections.Generic;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Models.ViewModels.Restaurant;

namespace FoodService.Services.RestaurantService
{
    public interface IRestaurantService
    {
        Task<Restaurant> GetRestaurantByIdAsync(long id);
        Task<Restaurant> SaveRestaurantAsync(RestaurantRequest restaurantReq, string managerName);
        Task<List<Restaurant>> FindAllAsync();
        Task<Restaurant> FindByIdAsync(long postId);
        Task<Restaurant> EditRestaurantAsync(long id, RestaurantRequest restaurantRequest);
        Task<List<Restaurant>> FindByManagerNameOrEmailAsync(string managerName);
        Task<bool> ValidateAccess(long restaurantId, string managerName);
        Task<EditRestaurantViewModel> BuildEditRestaurantViewModelAsync(long restaurantId);
        Task<EditRestaurantViewModel> BuildEditRestaurantViewModelAsync(long restaurantId, RestaurantRequest restaurantRequest);
    }
}