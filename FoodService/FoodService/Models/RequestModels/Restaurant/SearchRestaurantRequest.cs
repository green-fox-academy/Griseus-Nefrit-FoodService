using System.ComponentModel.DataAnnotations;

namespace FoodService.Models.RequestModels.Restaurant
{
    public class SearchRestaurantRequest
    {
        [Required]
        public string City { get; set; }

        public string MealName { get; set; }
    }
}
