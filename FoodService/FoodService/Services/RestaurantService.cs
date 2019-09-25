using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;

namespace FoodService.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationContext applicationContext;

        public RestaurantService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task<List<Restaurant>> findAll()
        {
            List<Restaurant> restaurantList = applicationContext.Restaurants.ToList();
            return restaurantList;
        }
    }

}
