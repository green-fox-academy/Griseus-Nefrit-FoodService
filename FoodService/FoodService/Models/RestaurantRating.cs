using FoodService.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models
{
    public class RestaurantRating
    {
        public long RestaurantRatingId { get; set; }
        public AppUser AppUser { get; set; }
        public Restaurant Restaurant { get; set; }
        public int Rating { get; set; }
        public string Oppinion { set; get; }
    }
}
