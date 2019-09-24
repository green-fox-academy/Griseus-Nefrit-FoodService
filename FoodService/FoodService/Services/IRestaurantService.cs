using System.Collections.Generic;
using System.Threading.Tasks;
using FoodService.Models;

namespace FoodService.Services
{
    public interface IRestaurantService
    {
        Task<Restaurant> getRestaurantByIdAsync(long id);
    }
}