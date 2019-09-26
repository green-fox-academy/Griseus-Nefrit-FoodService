using System.Collections.Generic;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;

namespace FoodService.Services.RestaurantService
{
    public interface IRestaurantService
    {
        Task<Restaurant> GetRestaurantByIdAsync(long id);
    }
}