﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public List<Restaurant> OwnedRestaurants { get; set; }
        public List<Order> ShoppingCarts { get; set; }
        public string TimezoneId { get; set; }
    }
}
