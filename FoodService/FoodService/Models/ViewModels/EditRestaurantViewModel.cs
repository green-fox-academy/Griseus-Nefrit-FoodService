using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;

namespace FoodService.ViewModels
{
    public class EditRestaurantViewModel
    {
      //  public Meal Meal { get; set; }
        public Restaurant Restaurant { get; set; }

        public AddMealRequest AddMealRequest { get; set; }
    }
}