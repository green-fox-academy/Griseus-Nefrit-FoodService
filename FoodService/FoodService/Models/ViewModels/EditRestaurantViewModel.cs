using FoodService.Models;
using FoodService.Models.RequestModels.Restaurant;

namespace FoodService.ViewModels
{
    public class EditRestaurantViewModel
    {

        public EditRestaurantViewModel()
        {
            AddMealRequest= new AddMealRequest();
            AddMealRequest.Price = new Price();
        }

        public Restaurant Restaurant { get; set; }

        public AddMealRequest AddMealRequest { get; set; }
    }
}