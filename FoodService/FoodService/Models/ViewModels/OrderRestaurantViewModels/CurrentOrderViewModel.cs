using System.Collections.Generic;

namespace FoodService.Models.ViewModels.OrderViewModels
{
    public class CurrentOrderViewModel
    {
        public List<Restaurant> RestaurantsOfManager { get; set; }
        public List<Order> Orders { get; set; }

    }
}