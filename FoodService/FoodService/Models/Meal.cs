using System.ComponentModel.DataAnnotations;

namespace FoodService.Models
{
    public class Meal
    {
        public long MealId { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        
     //   public long PriceId { get; set; }
        public Price Price { get; set; }

     //   public long RestaurantId { get; set; }
     
        public Restaurant Restaurant { get; set; }

    }
}
