using FoodService.Models.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace FoodService.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateSubmitted { get; set; }
        public DateTime DateProcessed { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<CartItem> CartItems { get; set; }
        public AppUser User { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Address Address { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
