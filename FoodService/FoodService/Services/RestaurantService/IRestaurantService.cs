using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.Identity;
using FoodService.Models.RequestModels.RestaurantRequestModels;
using FoodService.Models.ViewModels.RestaurantViewModels;
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
        Task<List<Restaurant>> FindRestaurantByManagerNameOrEmailAsync(string managerName);
        Task<bool> ValidateAccessAsync(long restaurantId, ClaimsPrincipal user);
        Task<EditRestaurantViewModel> BuildEditRestaurantViewModelAsync(long restaurantId);
        Task<EditRestaurantViewModel> BuildEditRestaurantViewModelAsync(long restaurantId, RestaurantRequest restaurantRequest);
        Task<List<String>> GetUniqueCitiesAsync();
        Task<PagingList<Restaurant>> GetRestaurantsByRequestAsync(int page, ClaimsPrincipal user, SearchRestaurantRequest searchRestaurantRequest);
        Task DeleteRestaurantAsync(long id);
        Task<List<Restaurant>> GetRestaurantsByManagerAsync(ClaimsPrincipal user);
        Task AddImageUriToRestaurantAsync(long RestaurantId, Microsoft.Azure.Storage.Blob.CloudBlockBlob blob);
    }
}