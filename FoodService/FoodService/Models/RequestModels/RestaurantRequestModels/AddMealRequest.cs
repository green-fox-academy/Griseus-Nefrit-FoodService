using System.ComponentModel.DataAnnotations;
using FoodService.Services.BlobService;
using Microsoft.AspNetCore.Http;

namespace FoodService.Models.RequestModels.RestaurantRequestModels
{
    public class AddMealRequest
    {
        [Required]
        public string Name { get; set; }
       
        [Required]
        public string Description { get; set; }

        [Required]
        public Price Price { get; set; }

        [Required]
        public long RestaurantId { get; set; }

        public IFormFile Image { get; set; }
    }
}