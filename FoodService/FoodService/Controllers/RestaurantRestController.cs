using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.Api;
using FoodService.Services.MealService;
using FoodService.Services.RestaurantService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodService.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantRestController : ControllerBase
    {
        private readonly IRestaurantService restaurantService;
        private readonly IMealService mealService;

        public RestaurantRestController(IRestaurantService restaurantService, IMealService mealService)
        {
            this.restaurantService = restaurantService;
            this.mealService = mealService;
        }

        // GET: api/restaurants
        [HttpGet]
        public async Task<IEnumerable<Restaurant>> Get([FromQuery] string city)
        {
            if (city != null)
            {
                return await restaurantService.GetRestaurantsByCityName(city);
            }
            else
            {
                return await restaurantService.FindAllAsync();
            }
        }

        // GET: api/restaurants/5
        [HttpGet("{id}")]
        public async Task<Restaurant> Get(long id)
        {
            var restaurant = await restaurantService.FindByIdOnlyRestaurantAsync(id);
            return restaurant;
        }

        // GET: api/restaurants/5/meals
        [HttpGet("{id}/meals")]
        public async Task<MealApi> GetMeals(long id)
        {
            var meals = new MealApi() { Meals = await mealService.GetMealByRestaurantIdAsync(id) };
            return meals;
        }
    }
}
