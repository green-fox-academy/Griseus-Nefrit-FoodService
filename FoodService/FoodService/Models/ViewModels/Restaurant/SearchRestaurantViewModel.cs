using FoodService.Models.RequestModels.Restaurant;
using ReflectionIT.Mvc.Paging;

namespace FoodService.Models.ViewModels.Restaurant
{
    public class SearchRestaurantViewModel
    {
        public PagingList<FoodService.Models.Restaurant> PagingList { get; set; }
        public SearchRestaurantRequest SearchRestaurantRequest { get; set; }
    }
}