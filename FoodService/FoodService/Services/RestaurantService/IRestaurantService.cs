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
        Task<Restaurant> FindByIdAsync(long postId);
        Task<Restaurant> EditRestaurantAsync(long id, RestaurantRequest request);
        Task<List<Restaurant>> findByManagerNameOrEmail(string managerName);
        Task<bool> ValidateAccess(long restaurantId, string managerName);
    }
}
