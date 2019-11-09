using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models.Api
{
    public class OrderApi
    {
        public string UserId { get; set; }
        public long RestaurantId { get; set; }
        public List<CartItemBasicApi> Meals { get; set; }
        public AddressApi AddressApi { get; set; }
    }
}
