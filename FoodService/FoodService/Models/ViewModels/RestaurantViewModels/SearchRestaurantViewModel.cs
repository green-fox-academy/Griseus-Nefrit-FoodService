using System;
using System.Collections.Generic;
using FoodService.Models.RequestModels.RestaurantRequestModels;
using ReflectionIT.Mvc.Paging;

namespace FoodService.Models.ViewModels.RestaurantViewModels
{
    public class SearchRestaurantViewModel
    {
        public PagingList<Restaurant> PagingList { get; set; }
        public SearchRestaurantRequest SearchRestaurantRequest { get; set; }
        public List<String> UniqueCities { get; set; } 
    }
}
