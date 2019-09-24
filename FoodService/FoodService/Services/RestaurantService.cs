using System.Collections.Generic;
using System.Linq;
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

        public Restaurant getRestaurantById(long id)
        {
            var restaurant = applicationContext.Restaurants.Include(t => t.Meals).FirstOrDefault(t => t.RestaurantId == id);
            if (restaurant == null)
            {
                return null;
            }
            return restaurant;
        }
    }
}