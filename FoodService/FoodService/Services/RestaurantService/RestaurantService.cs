using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Services.RestaurantService;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Services.RestaurantService
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationContext applicationContext;

        public RestaurantService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task<Restaurant> GetRestaurantByIdAsync(long id)
        {
            var restaurant = await applicationContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price).FirstOrDefaultAsync(t => t.RestaurantId == id);
            if (restaurant == null)
            {
                return null;
            }
            return restaurant;
        }
    }
}