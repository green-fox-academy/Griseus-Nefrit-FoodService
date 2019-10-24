using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models
{
    public enum OrderStatus
    {
        Draft = 1,
        Ordered,
        Completed
    }
}