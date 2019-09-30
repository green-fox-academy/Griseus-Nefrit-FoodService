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
        Task<List<Restaurant>> findAll();
        Task<Restaurant> FindByIdAsync(long postId);
        Task<Restaurant> EditRestaurantAsync(long id, RestaurantRequest restaurantRequest);
        Task<List<Restaurant>> findByManagerNameOrEmail(string managerName);
        Task<bool> ValidateAccess(long restaurantId, string managerName);
        Task<EditRestaurantViewModel> buildEditRestaurantViewModel(long restaurantId);
        Task<EditRestaurantViewModel> buildEditRestaurantViewModel(long restaurantId, RestaurantRequest restaurantRequest);
    }
}