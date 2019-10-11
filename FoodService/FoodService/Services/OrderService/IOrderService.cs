using FoodService.Models;
using FoodService.Models.Identity;
using FoodService.Models.RequestModels.OrderRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodService.Services.OrderService
{
    public interface IOrderService
    {
        Task<Order> GetOrderById(long orderId);
        Task<Order> AddMealToOrderAsync(long mealId, string userName);
        Task<Order> GetShoppingCartByUserAndRestaurantAsync(string userName, long restaurantId);
        Task<ShoppingCartRequest> CreateShoppingCartRequestByUserAndRestaurantAsync(string userName, Address address, long restaurantId);
        Task<ShoppingCartRequest> CreateShoppingCartRequestByIdAsync(Address address, long orderId);
        Task<CartItem> GetCartItemByIdAsync(long cartItemId);
        Task DeleteCartItemAsync(long cartItemId);
        Task SaveOrderAsync(long orderId, Address address);
        Task<int> GetNumberOfItemsInBasket(string userName, long restaurantId);
        Task<bool> ValidateAccessAsync(long cartItemId, string userName);
        Task<List<Order>> GetOrdersByManagerAsync(ClaimsPrincipal user);
    }
}
