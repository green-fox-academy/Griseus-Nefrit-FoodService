using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Models.ViewModels.Restaurant;
using ReflectionIT.Mvc.Paging;

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
        Task<bool> ValidateAccessAsync(long restaurantId, string managerName);
        Task<EditRestaurantViewModel> BuildEditRestaurantViewModelAsync(long restaurantId);
        Task<EditRestaurantViewModel> BuildEditRestaurantViewModelAsync(long restaurantId, RestaurantRequest restaurantRequest);
        Task<List<String>> GetUniqueCities();
        Task<PagingList<Restaurant>> GetRestaurantsByRequestAsync(int page, ClaimsPrincipal user, SearchRestaurantRequest searchRestaurantRequest);
    }
}