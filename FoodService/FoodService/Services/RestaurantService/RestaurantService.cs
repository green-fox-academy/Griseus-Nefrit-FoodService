using System.Threading.Tasks;
using FoodService.Models;
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