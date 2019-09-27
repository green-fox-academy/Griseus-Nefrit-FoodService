using System.Collections.Generic;

namespace FoodService.Models
{
    public class Restaurant
    {
        public long RestaurantId { get; set; }
        
        public List<Meal> Meals { get; set; }
    }
}