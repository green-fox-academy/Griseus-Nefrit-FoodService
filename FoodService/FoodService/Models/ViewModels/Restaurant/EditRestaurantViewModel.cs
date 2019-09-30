﻿using FoodService.Models.RequestModels.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models.ViewModels.Restaurant
{
    public class EditRestaurantViewModel
    {
        public RestaurantRequest RestaurantRequest { get; set; }
        public List<Meal> Meals { get; set; }
        public long RestaurantId { get; set; }
    }
}
