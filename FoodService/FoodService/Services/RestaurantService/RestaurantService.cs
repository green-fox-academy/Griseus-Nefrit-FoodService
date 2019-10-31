using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.RestaurantRequestModels;
using FoodService.Services.User;
using Microsoft.EntityFrameworkCore;
using FoodService.Models.ViewModels.RestaurantViewModels;
using ReflectionIT.Mvc.Paging;
using AutoMapper;
using FoodService.Services.BlobService;
using Microsoft.Azure.Storage.Blob;
using FoodService.Models.Identity;

namespace FoodService.Services.RestaurantService
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IBlobStorageService blobStorageService;

        public RestaurantService(ApplicationDbContext applicationDbContext, IUserService userService, IMapper mapper, IBlobStorageService blobStorageService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userService = userService;
            this.mapper = mapper;
            this.blobStorageService = blobStorageService;
        }

        public async Task<Restaurant> GetRestaurantByIdAsync(long id)
        {
            var restaurant = await applicationDbContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price).Include(t => t.Manager).FirstOrDefaultAsync(t => t.RestaurantId == id);
            if (restaurant == null)
            {
                return null;
            }
            return restaurant;
        }
        public async Task<Restaurant> SaveRestaurantAsync(RestaurantRequest restaurantReq, string managerName)
        {
            var manager = await userService.FindUserByNameOrEmailAsync(managerName);
            var restaurant = mapper.Map<RestaurantRequest, Restaurant>(restaurantReq);
            restaurant.Manager = manager;
            await applicationDbContext.Restaurants.AddAsync(restaurant);
            await applicationDbContext.SaveChangesAsync();
            if (restaurantReq.Image == null)
            {
                restaurant.ImageUri = "https://dotnetpincerstorage.blob.core.windows.net/mealimages/default/default.png";
            }
            else
            {
                CloudBlockBlob blob = await blobStorageService.MakeBlobFolderAndSaveImageAsync(int.MaxValue - restaurant.RestaurantId, restaurantReq.Image);
                await AddImageUriToRestaurantAsync(restaurant.RestaurantId, blob);
            }
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
            var restaurantList = await applicationDbContext.Restaurants.Include(r => r.Meals).AsQueryable().Where(r => r.Manager.UserName == managerName).OrderBy(r => r.Name).ToListAsync();
            return restaurantList;
        }

        public async Task<Restaurant> EditRestaurantAsync(long id, RestaurantRequest restaurantRequest)
        {
            var editedRestaurant = await GetRestaurantByIdAsync(id);
            editedRestaurant = mapper.Map<RestaurantRequest, Restaurant>(restaurantRequest, editedRestaurant);
            if (restaurantRequest.Image != null)
            {
                CloudBlockBlob blob = await blobStorageService.MakeBlobFolderAndSaveImageAsync(int.MaxValue - editedRestaurant.RestaurantId, restaurantRequest.Image);
                await AddImageUriToRestaurantAsync(id, blob);
            }
            await applicationDbContext.SaveChangesAsync();
            return editedRestaurant;
        }

        public async Task<Restaurant> FindByIdAsync(long restaurantId)
        {
            return await applicationDbContext.Restaurants.Include(r => r.Meals).FirstOrDefaultAsync(p => p.RestaurantId == restaurantId);
        }

        public async Task<bool> ValidateAccessAsync(long restaurantId, ClaimsPrincipal user)
        {
            if (user.IsInRole("Admin"))
            {
                return true;
            }
            else
            {
                List<Restaurant> ownedRestaurants = await FindRestaurantByManagerNameOrEmailAsync(user.Identity.Name);
                Restaurant currentRestaurant = await FindByIdAsync(restaurantId);
                return ownedRestaurants.Contains(currentRestaurant);
            }
        }

        public async Task<EditRestaurantViewModel> BuildEditRestaurantViewModelAsync(long restaurantId)
        {
            var restaurant = await GetRestaurantByIdAsync(restaurantId);
            var editRestaurantViewModel = new EditRestaurantViewModel()
            {
                RestaurantRequest = mapper.Map<Restaurant, RestaurantRequest>(restaurant),
                Meals = restaurant.Meals,
                RestaurantId = restaurant.RestaurantId
            };

            await applicationDbContext.SaveChangesAsync();
            return editRestaurantViewModel;
        }

        public async Task<EditRestaurantViewModel> BuildEditRestaurantViewModelAsync(long restaurantId, RestaurantRequest restaurantRequest)
        {
            var restaurant = await GetRestaurantByIdAsync(restaurantId);
            var editRestaurantViewModel = new EditRestaurantViewModel()
            {
                RestaurantRequest = restaurantRequest,
                Meals = restaurant.Meals,
                RestaurantId = restaurant.RestaurantId
            };

            await applicationDbContext.SaveChangesAsync();
            return editRestaurantViewModel;
        }

        public async Task<List<String>> GetUniqueCitiesAsync()
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

        public async Task<List<Restaurant>> FindRestaurantsByCityAsync(SearchRestaurantRequest searchRestaurantRequest)
        {
            var restaurants = await applicationDbContext.Restaurants.Where(r => r.City == searchRestaurantRequest.City).ToListAsync();
            return restaurants;
        }

        public async Task<PagingList<Restaurant>> GetRestaurantsByRequestAsync(int page, ClaimsPrincipal user, SearchRestaurantRequest searchRestaurantRequest)
        {
            var restaurants = await applicationDbContext.Restaurants.Include(r => r.Meals).ToListAsync();
            var filteredRestaurantsList = restaurants.Where(r => r.City.Equals(searchRestaurantRequest.City) || String.IsNullOrEmpty(searchRestaurantRequest.City)).OrderBy(r => r.Name).ToList();
            var restaurantQuery = new List<Restaurant>();

            if (String.IsNullOrEmpty(searchRestaurantRequest.MealName))
            {
                restaurantQuery = filteredRestaurantsList;
            }
            else
            {
                foreach (Restaurant restaurant in filteredRestaurantsList)
                {
                    foreach (Meal meal in restaurant.Meals)
                    {
                        if (meal.Name.ToLower().Contains(searchRestaurantRequest.MealName.ToLower()))
                        {
                            restaurantQuery.Add(restaurant);
                            break;
                        }
                    }
                }
            }
            return PagingList.Create(restaurantQuery, 10, page);
        }

        public async Task DeleteRestaurantAsync(long id)
        {
            var restaurant = await FindByIdAsync(id);
            blobStorageService.DeleteBlobFolder(int.MaxValue - id);
            for (int i = 0; i < restaurant.Meals.Count; i++)
            {
                blobStorageService.DeleteBlobFolder(restaurant.Meals[i].MealId);
                applicationDbContext.Meals.Remove(restaurant.Meals[i]);
            }
            foreach (var o in applicationDbContext.Orders.Where(f => f.Restaurant.RestaurantId == restaurant.RestaurantId))
            {
                applicationDbContext.Orders.Remove(o);
            }
            applicationDbContext.Restaurants.Remove(restaurant);
            await applicationDbContext.SaveChangesAsync();
        }
        public async Task<List<Restaurant>> GetRestaurantsByManagerAsync(ClaimsPrincipal user)
        {
            var restaurantsOfManager = await applicationDbContext.Restaurants.Where(r => r.Manager.UserName == user.Identity.Name).ToListAsync();
            return restaurantsOfManager;
        }
        public async Task AddImageUriToRestaurantAsync(long RestaurantId, CloudBlockBlob blob)
        {
            var restaurant = await GetRestaurantByIdAsync(RestaurantId);
            restaurant.ImageUri = blob.SnapshotQualifiedStorageUri.PrimaryUri.ToString();
        }

        public async Task SaveUserRatingAsync(string username, int Stars, long restaurantId, string Oppinion)
        {
            Restaurant restaurant = await GetRestaurantByIdAsync(restaurantId);
            var appUser = await userService.FindUserByNameOrEmailAsync(username);
            RestaurantRating restaurantRating = new RestaurantRating
            {
                Restaurant = restaurant,
                Rating = Stars,
                AppUser = appUser,
                Oppinion = Oppinion
            };
            applicationDbContext.RestaurantRatings.Add(restaurantRating);
            applicationDbContext.SaveChanges();
        }

        public async Task<bool> UserAlreadyRatedThisRestaurantAsync(string username, long id)
        {
            Restaurant restaurant = await GetRestaurantByIdAsync(id);
            var appUser = await userService.FindUserByNameOrEmailAsync(username);
            var restaurantrating = applicationDbContext.RestaurantRatings.Where(r => r.Restaurant == restaurant && r.AppUser == appUser);
            if (restaurantrating.FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }
    }
}
