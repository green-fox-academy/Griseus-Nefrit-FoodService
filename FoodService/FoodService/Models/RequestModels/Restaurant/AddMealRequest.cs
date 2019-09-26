using System.ComponentModel.DataAnnotations;

namespace FoodService.Models.RequestModels.Restaurant
{
    public class AddMealRequest
    {
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }
       
        [Required(ErrorMessage = "Description required")]
        public string Description { get; set; }
       
        /*
        [Required(ErrorMessage = "Price required")]
        public int Amount { get; set; }
        
        [Required(ErrorMessage = "Currency required")]
        public string Currency { get; set; } = "Ft";
        */
        
    //    public Price Price { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }

        public long RestaurantId { get; set; }
    }
}