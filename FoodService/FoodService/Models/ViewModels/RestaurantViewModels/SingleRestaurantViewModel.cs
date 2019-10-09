using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models.ViewModels.RestaurantViewModels
{
    public class SingleRestaurantViewModel
    {
        public Restaurant Restaurant { get; set; }
        public int NumberOfCartItems { get; set; }
    }
}
