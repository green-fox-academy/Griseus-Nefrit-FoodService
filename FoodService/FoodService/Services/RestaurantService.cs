using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public RestaurantService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<List<Restaurant>> findAll()
        {
            List<Restaurant> restaurantList = await applicationDbContext.Restaurants.ToListAsync();
            return restaurantList;
        }
    }

}
