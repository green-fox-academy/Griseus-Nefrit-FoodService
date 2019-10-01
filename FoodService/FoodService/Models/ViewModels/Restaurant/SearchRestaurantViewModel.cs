using ReflectionIT.Mvc.Paging;
using FoodService.Models.RequestModels.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models.ViewModels.Restaurant
{
    public class SearchRestaurantViewModel
    {
        public PagingList<FoodService.Models.Restaurant> PagingList { get; set; }
        public SearchRestaurantRequest SearchRestaurantRequest { get; set; }
    }
}
