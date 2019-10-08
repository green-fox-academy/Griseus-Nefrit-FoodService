using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models
{
    public class CartItem
    {
        public long CartItemId { get; set; }
        public int Quantity { get; set; }
        public Meal meal { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

    }
}
