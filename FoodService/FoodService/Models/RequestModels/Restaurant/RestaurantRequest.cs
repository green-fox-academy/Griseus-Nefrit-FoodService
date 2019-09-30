using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FoodService.Models.RequestModels.Restaurant
{
    public class RestaurantRequest
    {
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
    }
}
