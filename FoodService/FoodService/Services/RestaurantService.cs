using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationContext applicationContext;

        public RestaurantService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task<Restaurant> getRestaurantByIdAsync(long id)
        {
            var restaurant = await applicationContext.Restaurants.Include(t => t.Meals).FirstOrDefaultAsync(t => t.RestaurantId == id);
            if (restaurant == null)
            {
                return null;
            }
            return restaurant;
        }
    }
}