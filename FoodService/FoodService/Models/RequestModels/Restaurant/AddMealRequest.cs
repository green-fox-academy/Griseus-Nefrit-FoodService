using System.ComponentModel.DataAnnotations;

namespace FoodService.Models.RequestModels.Restaurant
{
    public class AddMealRequest
    {
        [Required]
        public string Name { get; set; }
       
        [Required]
        public string Description { get; set; }

        [Required]
        public Price Price { get; set; }

        [Required]
        public long RestaurantId { get; set; }
    }
}