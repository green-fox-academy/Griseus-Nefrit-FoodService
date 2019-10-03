using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Services.User;
using Microsoft.EntityFrameworkCore;
using FoodService.Models.ViewModels.Restaurant;
using ReflectionIT.Mvc.Paging;


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

        public async Task<List<Restaurant>> FindAllAsync()
        {
            List<Restaurant> restaurantList = await applicationDbContext.Restaurants.AsQueryable().OrderBy(r => r.Name).ToListAsync();
            return restaurantList;
        }

        public async Task<List<Restaurant>> FindByManagerNameOrEmailAsync(string managerName)
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

        public async Task<bool> ValidateAccessAsync(long restaurantId, string managerName)
        {
            List<Restaurant> ownedRestaurants = await FindByManagerNameOrEmailAsync(managerName);
            Restaurant currentRestaurant = await FindByIdAsync(restaurantId);
            return ownedRestaurants.Contains(currentRestaurant);
        }

        public async Task<EditRestaurantViewModel> BuildEditRestaurantViewModelAsync(long restaurantId)
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

        public async Task<List<Restaurant>> FindRestaurantsByCity(SearchRestaurantRequest searchRestaurantRequest)
        {
            var restaurants = await applicationDbContext.Restaurants.Where(r => r.City == searchRestaurantRequest.City).ToListAsync();
            return restaurants;
        }

        public async Task<PagingList<Restaurant>> GetRestaurantsByRequestAsync(int page, ClaimsPrincipal user, SearchRestaurantRequest searchRestaurantRequest)
        {
            if (user.IsInRole("Manager"))
            {
                var restaurantManager = await FindByManagerNameOrEmailAsync(user.Identity.Name);
                return PagingList.Create(restaurantManager, 10, page);
            }
            if (String.Equals("Choose a city", searchRestaurantRequest.City))
            {
                searchRestaurantRequest.City = null;
            }
            var restaurants = await applicationDbContext.Restaurants.Include(r => r.Meals).Where(r => r.City.Equals(searchRestaurantRequest.City)
                            || String.IsNullOrEmpty(searchRestaurantRequest.City)).OrderBy(r => r.Name).ToListAsync();
            var restaurantQuery = new List<Restaurant>();
            if (String.IsNullOrEmpty(searchRestaurantRequest.MealName) || String.IsNullOrEmpty(searchRestaurantRequest.City))
            {
                restaurantQuery = restaurants;
            }
            else
            {
                foreach (Restaurant restaurant in restaurants)
                {
                    foreach (Meal meal in restaurant.Meals)
                    {
                        if(meal.Name.Contains(searchRestaurantRequest.MealName))
                        {
                            restaurantQuery.Add(restaurant);
                            break;
                        }
                    }
                }
            }
            return PagingList.Create(restaurantQuery, 10, page);
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
    }
}