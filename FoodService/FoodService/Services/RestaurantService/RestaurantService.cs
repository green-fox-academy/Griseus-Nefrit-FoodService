using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Services.User;
using Microsoft.EntityFrameworkCore;
using FoodService.Models.ViewModels.Restaurant;

namespace FoodService.Services.RestaurantService
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

        public async Task<Restaurant> GetRestaurantByIdAsync(long id)
        {
            var restaurant = await applicationDbContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price).FirstOrDefaultAsync(t => t.RestaurantId == id);
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

        public async Task<Restaurant> EditRestaurantAsync(long id, RestaurantRequest restaurantRequest)
        {
            var editedRestaurant = await GetRestaurantByIdAsync(id);
            editedRestaurant.Name = restaurantRequest.Name;
            editedRestaurant.Description = restaurantRequest.Description;
            editedRestaurant.City = restaurantRequest.City;
            editedRestaurant.FoodType = restaurantRequest.FoodType;
            editedRestaurant.PriceCategory = restaurantRequest.PriceCategory;
            await applicationDbContext.SaveChangesAsync();
            return editedRestaurant;
        }

        public async Task<Restaurant> FindByIdAsync(long restaurantId)
        {
            return await applicationDbContext.Restaurants.FirstOrDefaultAsync(p => p.RestaurantId == restaurantId);
        }

        public async Task<bool> ValidateAccess(long restaurantId, string managerName)
        {
            List<Restaurant> ownedRestaurants = await findByManagerNameOrEmail(managerName);
            Restaurant currentRestaurant = await FindByIdAsync(restaurantId);
            if (ownedRestaurants.Contains(currentRestaurant))
            {
                return true;
            }
            return false;
        }

        public async Task<EditRestaurantViewModel> buildEditRestaurantViewModel(long restaurantId)
        {
            var restaurant = await GetRestaurantByIdAsync(restaurantId);
            var editRestauratnViewModel = new EditRestaurantViewModel()
            {
                RestaurantRequest = new RestaurantRequest()
                {
                    Name = restaurant.Name,
                    Description = restaurant.Description,
                    City = restaurant.City,
                    FoodType = restaurant.FoodType,
                    PriceCategory = restaurant.PriceCategory
                },
                Meals = restaurant.Meals,
                RestaurantId = restaurant.RestaurantId
            };
            
            await applicationDbContext.SaveChangesAsync();
            return editRestauratnViewModel;
        }

        public async Task<EditRestaurantViewModel> buildEditRestaurantViewModel(long restaurantId, RestaurantRequest restaurantRequest)
        {
            var restaurant = await GetRestaurantByIdAsync(restaurantId);
            var editRestauratnViewModel = new EditRestaurantViewModel()
            {
                RestaurantRequest = restaurantRequest,
                Meals = restaurant.Meals,
                RestaurantId = restaurant.RestaurantId
            };

            await applicationDbContext.SaveChangesAsync();
            return editRestauratnViewModel;
        }

        public async Task<List<Restaurant>> findByFoodNameAsync(string foodName)
        {
            var restaurantList = await applicationDbContext.Restaurants.AsQueryable().Where(r => r.FoodType.Contains(foodName)).OrderBy(r => r.Name).ToListAsync();
            return restaurantList;
        }
    }
}