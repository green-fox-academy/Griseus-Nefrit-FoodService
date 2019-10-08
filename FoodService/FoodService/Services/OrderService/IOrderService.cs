using FoodService.Models;
using FoodService.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.OrderService
{
    public interface IOrderService
    {
        Task<ShoppingCart> AddMealToOrderAsync(long mealId, string userName);
        Task<ShoppingCart> GetShoppingCartByUserAsync(string userName);
    }
}
