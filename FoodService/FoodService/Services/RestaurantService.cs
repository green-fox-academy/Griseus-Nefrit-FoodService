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

        public async Task<Restaurant> SaveRestaurantAsync(RestaurantRequest restaurantReq, long id)
        {
            var restaurant = new Restaurant();
            restaurant.Name = restaurantReq.Name;
            restaurant.Description = restaurantReq.Description;
            restaurant.City = restaurantReq.City;
            restaurant.FoodType = restaurantReq.FoodType;
            restaurant.PriceCategory = restaurantReq.PriceCategory;
            await applicationDbContext.Restaurants.AddAsync(restaurant);
            await applicationDbContext.SaveChangesAsync();
            return restaurant;
        }

        public async Task<List<Restaurant>> findAll()
        {
            List<Restaurant> restaurantList = await applicationDbContext.Restaurants.ToListAsync();
            return restaurantList;
        }
    }
}
