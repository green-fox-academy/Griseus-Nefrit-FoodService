using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Services.User;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IUserService userService;

        public RestaurantService(ApplicationDbContext applicationDbContext, IUserService userService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userService = userService;
        }

        public async Task<Restaurant> SaveRestaurantAsync(RestaurantRequest restaurantReq, string managerName)
        {
            var manager = await userService.FindUserByNameOrEmail(managerName);
            var restaurant = new Restaurant
            {
                Name = restaurantReq.Name,
                Description = restaurantReq.Description,
                City = restaurantReq.City,
                FoodType = restaurantReq.FoodType,
                PriceCategory = restaurantReq.PriceCategory,
                Manager = manager
            };
            await applicationDbContext.Restaurants.AddAsync(restaurant);
            await applicationDbContext.SaveChangesAsync();
            return restaurant;
        }

        public async Task<List<Restaurant>> findAll()
        {
            List<Restaurant> restaurantList = await applicationDbContext.Restaurants.AsQueryable().OrderBy(r => r.Name).ToListAsync();
            return restaurantList;
        }

        public async Task<List<Restaurant>> findByManagerNameOrEmail(string managerName)
        {
            var restaurantList = await applicationDbContext.Restaurants.AsQueryable().Where(r => r.Manager.UserName == managerName).OrderBy(r => r.Name).ToListAsync();
            return restaurantList;
        }
    }
}
