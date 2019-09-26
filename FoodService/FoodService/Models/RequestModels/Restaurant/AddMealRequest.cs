using System.ComponentModel.DataAnnotations;

namespace FoodService.Models.RequestModels.Restaurant
{
    public class AddMealRequest
    {
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }
       
        [Required(ErrorMessage = "Description required")]
        public string Description { get; set; }

        public Price Price { get; set; }

        public long RestaurantId { get; set; }
    }
}