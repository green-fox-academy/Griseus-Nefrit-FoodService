using FoodService.Models.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodService.Models
{
    public class Restaurant
    {
        public long RestaurantId { get; set; }
        public List<Meal> Meals { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name of the restaurant:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [Display(Name = "Description of the restaurant:")]
        public string Description { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [Display(Name = "City:")]
        public string City { get; set; }

        [Required(ErrorMessage = "Food type is required.")]
        [Display(Name = "Food type:")]
        public string FoodType { get; set; }

        [Required]
        [Display(Name = "Price range:")]
        [EnumDataType(typeof(PriceCategory), ErrorMessage = "Value should be between 1 and 4.")]
        public PriceCategory PriceCategory { get; set; }

        [Required]
        public AppUser Manager { get; set; }
        public string ImageUri { get; set; }
    }
}
