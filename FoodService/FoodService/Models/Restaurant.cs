using FoodService.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models
{
    public class Restaurant
    {
        public long RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string FoodType { get; set; }
        public PriceCategory PriceCategory { get; set; }
        [Required]
        public AppUser Manager { get; set; }
    }
}
