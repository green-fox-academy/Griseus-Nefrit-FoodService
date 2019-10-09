using FoodService.Models;
using FoodService.Models.Identity;
using FoodService.Models.RequestModels.OrderRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.OrderService
{
    public interface IOrderService
    {
        Task<Order> GetOrderById(long orderId);
        Task<Order> AddMealToOrderAsync(long mealId, string userName);
        Task<Order> GetShoppingCartByUserAsync(string userName);
        Task<ShoppingCartRequest> CreateShoppingCartRequestByUserAsync(string userName, Address address);
        Task<CartItem> GetCartItemByIdAsync(long cartItemId);
        Task DeleteCartItemAsync(long cartItemId);
        Task SaveOrderAsync(long orderId, Address address);
        Task<int> GetNumberOfItemsInBasket(string userName);
        Task<bool> ValidateAccessAsync(long cartItemId, string userName);
    }
}
