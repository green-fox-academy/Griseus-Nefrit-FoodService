using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models.RequestModels.Restaurant
{
    public class SearchRestaurantRequest
    {
        [Required]
        public string City { get; set; }

        public string FoodName { get; set; }
    }
}
