using System;
using System.Collections.Generic;
using System.Diagnostics;
using FoodService.Models;
using FoodService.Models.ViewModels;

namespace OrderViewModels
{
    public class CurrentOrderViewModel
    {
        public List<Restaurant> RestaurantsOfManager { get; set; }
        public List<Order> Orders { get; set; }
    }
}