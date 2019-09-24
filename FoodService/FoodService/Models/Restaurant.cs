using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FoodService.Models
{
    public class Restaurant
    {
        public long RestaurantId { get; set; }
        
        public List<Meal> Meals { get; set; }
    }
}