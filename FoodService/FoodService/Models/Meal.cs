using System.ComponentModel.DataAnnotations;

namespace FoodService.Models
{
    public class Meal
    {
        public long MealId { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        
        [Required]
        public Price Price { get; set; }

        public long RestaurantId { get; set; }

    }
}
