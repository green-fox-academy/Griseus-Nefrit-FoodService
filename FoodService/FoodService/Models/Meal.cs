
using FoodService.Services.BlobService;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace FoodService.Models
{
    public class Meal
    {
        public long MealId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Price Price { get; set; }
        public Restaurant Restaurant { get; set; }
        public string ImageUri { get; set; }
        public List<CartItem> CartItems { get; set; }

    }
}
