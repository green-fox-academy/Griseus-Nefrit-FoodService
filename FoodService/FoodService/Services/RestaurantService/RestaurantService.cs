using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Services.User;
using Microsoft.EntityFrameworkCore;
using FoodService.Models.ViewModels.Restaurant;
using ReflectionIT.Mvc.Paging;
using AutoMapper;

namespace FoodService.Services.RestaurantService
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IUserService userService;
        private readonly IMapper iMapper;

        public RestaurantService(ApplicationDbContext applicationDbContext, IUserService userService, IMapper iMapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.userService = userService;
            this.iMapper = iMapper;
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
            var restaurant = iMapper.Map<RestaurantRequest, Restaurant>(restaurantReq);
            restaurant.Manager = manager;
            await applicationDbContext.Restaurants.AddAsync(restaurant);
            await applicationDbContext.SaveChangesAsync();
            return restaurant;
        }

        public async Task<List<Restaurant>> FindAllAsync()
        {
            List<Restaurant> restaurantList = await applicationDbContext.Restaurants.AsQueryable().OrderBy(r => r.Name).ToListAsync();
            return restaurantList;
        }

        public async Task<List<Restaurant>> FindRestaurantByManagerNameOrEmailAsync(string managerName)
        {
            var restaurantList = await applicationDbContext.Restaurants.AsQueryable().Where(r => r.Manager.UserName == managerName).OrderBy(r => r.Name).ToListAsync();
            return restaurantList;
        }

        public async Task<Restaurant> EditRestaurantAsync(long id, RestaurantRequest restaurantRequest)
        {
            var editedRestaurant = await GetRestaurantByIdAsync(id);
            editedRestaurant = iMapper.Map<RestaurantRequest, Restaurant>(restaurantRequest, editedRestaurant);
            await applicationDbContext.SaveChangesAsync();
            return editedRestaurant;
        }

        public async Task<Restaurant> FindByIdAsync(long restaurantId)
        {
            return await applicationDbContext.Restaurants.FirstOrDefaultAsync(p => p.RestaurantId == restaurantId);
        }

        public async Task<bool> ValidateAccessAsync(long restaurantId, string managerName)
        {
            List<Restaurant> ownedRestaurants = await FindRestaurantByManagerNameOrEmailAsync(managerName);
            Restaurant currentRestaurant = await FindByIdAsync(restaurantId);
            return ownedRestaurants.Contains(currentRestaurant);
        }

        public async Task<EditRestaurantViewModel> BuildEditRestaurantViewModelAsync(long restaurantId)
        {
            var restaurant = await GetRestaurantByIdAsync(restaurantId);
            var editRestauratnViewModel = new EditRestaurantViewModel()
            {
                RestaurantRequest = iMapper.Map<Restaurant, RestaurantRequest>(restaurant),
                Meals = restaurant.Meals,
                RestaurantId = restaurant.RestaurantId
            };
            
            await applicationDbContext.SaveChangesAsync();
            return editRestauratnViewModel;
        }

        public async Task<EditRestaurantViewModel> BuildEditRestaurantViewModelAsync(long restaurantId, RestaurantRequest restaurantRequest)
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
        
        public async Task<List<String>> GetUniqueCities()
        {
            var restaurants = await FindAllAsync();
            var listCities = new List<String>();
            for (var i = 0; i < restaurants.Count; i++)
            {
                listCities.Add(restaurants[i].City);
            }
            
            var uniqueCities = listCities.Distinct().ToList();
            return uniqueCities;
        }

        public async Task<PagingList<Restaurant>> GetRestaurantsByRequestAsync( int page, ClaimsPrincipal user, SearchRestaurantRequest searchRestaurantRequest)
        {
            if (user.IsInRole("Manager"))
            {
                var restaurantsOfManager = await FindRestaurantByManagerNameOrEmailAsync(user.Identity.Name);
                return PagingList.Create(restaurantsOfManager, 4, page);
            }
            
            var restaurants = await applicationDbContext.Restaurants.Where(r => r.City.Equals(searchRestaurantRequest.City) || String.IsNullOrEmpty(searchRestaurantRequest.City)).ToListAsync();
            return PagingList.Create(restaurants, 4, page);
        }
    }
}