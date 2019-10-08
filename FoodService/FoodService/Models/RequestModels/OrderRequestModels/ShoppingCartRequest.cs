using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models.RequestModels.OrderRequestModels
{
    public class ShoppingCartRequest
    {
        public long ShoppingCartId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public Address Address { get; set; }
    }
}
