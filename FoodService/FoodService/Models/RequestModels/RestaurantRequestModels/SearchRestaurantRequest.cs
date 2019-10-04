using System.ComponentModel.DataAnnotations;

namespace FoodService.Models.RequestModels.RestaurantRequestModels
{
    public class SearchRestaurantRequest
    {
        [Required]
        public string City { get; set; }

        public string MealName { get; set; }
    }
}
