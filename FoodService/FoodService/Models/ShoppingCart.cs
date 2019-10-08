using FoodService.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models
{
    public class ShoppingCart
    {
        public long ShoppingCartId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<CartItem> CartItems { get; set; }
        public AppUser User { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
