using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models.Api
{
    public class CartItemBasicApi
    {
        public long MealId { get; set; }
        public int Quantity { get; set; }
    }
}
