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
        private readonly ApplicationDbContext applicationContext;

        public RestaurantService(ApplicationDbContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task<Restaurant> SaveRestaurantAsync(RestaurantRequest restaurantReq, long id)
        {
            var restaurant = new Restaurant();
            restaurant.Name = restaurantReq.Name;
            restaurant.Description = restaurantReq.Description;
            restaurant.City = restaurantReq.City;
            restaurant.FoodType = restaurantReq.FoodType;
            restaurant.PriceCategory = restaurantReq.PriceCategory;
            await applicationContext.Restaurants.AddAsync(restaurant);
            await applicationContext.SaveChangesAsync();
            return restaurant;
        }
    }
}
