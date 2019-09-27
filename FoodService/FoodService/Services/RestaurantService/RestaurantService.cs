using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Services.User;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Services.RestaurantService
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUserService userService;

        public RestaurantService(ApplicationDbContext applicationDbContext, IUserService userService)
        {
            this._applicationDbContext = applicationDbContext;
            this.userService = userService;
        }

        public async Task<Restaurant> GetRestaurantByIdAsync(long id)
        {
            var restaurant = await _applicationDbContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price).FirstOrDefaultAsync(t => t.RestaurantId == id);
            if (restaurant == null)
            {
                return null;
            }
            return restaurant;
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
            await _applicationDbContext.Restaurants.AddAsync(restaurant);
            await _applicationDbContext.SaveChangesAsync();
            return restaurant;
        }

        public async Task<List<Restaurant>> findAll()
        {
            List<Restaurant> restaurantList = await _applicationDbContext.Restaurants.AsQueryable().OrderBy(r => r.Name).ToListAsync();
            return restaurantList;
        }

        public async Task<List<Restaurant>> findByManagerNameOrEmail(string managerName)
        {
            var restaurantList = await _applicationDbContext.Restaurants.AsQueryable().Where(r => r.Manager.UserName == managerName).OrderBy(r => r.Name).ToListAsync();
            return restaurantList;
        }
    }
}