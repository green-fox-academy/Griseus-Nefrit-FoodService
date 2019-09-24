using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models
{
    public enum Category
    {
        ONE = 1,
        TWO,
        THREE,
        FOUR
    }
    public class Restaurant
    {
        public long RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string FoodType { get; set; }
        public Category PriceCategory { get; set; }
    }
}
