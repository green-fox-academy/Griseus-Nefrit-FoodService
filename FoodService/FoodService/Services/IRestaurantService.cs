using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
﻿using FoodService.Models;

namespace FoodService.Services
{
    public interface IRestaurantService
    {
        Task<Restaurant> SaveRestaurantAsync(RestaurantRequest restaurantReq, string managerName);
        Task<List<Restaurant>> findAll();
        Task<List<Restaurant>> findByManagerNameOrEmail(string managerName);
    }
}
