using System;
using System.ComponentModel.DataAnnotations;

namespace FoodService.Models
{
    public class Price
    {
        public long PriceId { get; set; }
        
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "The Price field should be greater than zero.")]
        public int Amount { get; set; }
        [Required]
        public string Currency { get; set; } = "Ft";
    }
}