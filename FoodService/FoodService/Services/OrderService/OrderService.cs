using AutoMapper;
using FoodService.Models;
using FoodService.Models.Identity;
using FoodService.Models.RequestModels.OrderRequestModels;
using FoodService.Services.MealService;
using FoodService.Services.RestaurantService;
using FoodService.Services.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace FoodService.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IUserService userService;
        private readonly IMealService mealService;
        private readonly IRestaurantService restaurantService;
        private readonly IMapper mapper;
        private Timer Timer;

        public OrderService(ApplicationDbContext applicationDbContext, IUserService userService, IMealService mealService, IRestaurantService restaurantService, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.userService = userService;
            this.mealService = mealService;
            this.restaurantService = restaurantService;
            this.mapper = mapper;
        }

        public async Task<Order> AddMealToOrderAsync(long mealId, string userName)
        {
            var meal = await mealService.GetMealByIdAsync(mealId);
            var shoppingCart = await GetShoppingCartByUserAndRestaurantAsync(userName, meal.Restaurant.RestaurantId);
            if(shoppingCart != null)
            {
                if(meal != null)
                {
                    var cartItem = await applicationDbContext.CartItems
                        .FirstOrDefaultAsync(c => c.Meal == meal && c.Order == shoppingCart);
                    if(cartItem == null)
                    {
                        var newCartItem = new CartItem()
                        {
                            Quantity = 1,
                            Meal = meal,
                            Order = shoppingCart
                        };
                        await applicationDbContext.CartItems.AddAsync(newCartItem);
                    } else
                    {
                        cartItem.Quantity++;
                    }
                    await applicationDbContext.SaveChangesAsync();
                }
            }
            return shoppingCart;
        }

        public async Task<Order> GetShoppingCartByUserAndRestaurantAsync(string userName, long restaurantId)
        {
            AppUser user = await userService.FindUserByNameOrEmailAsync(userName);
            var restaurant = await restaurantService.FindByIdAsync(restaurantId);
            if (user != null)
            {
                var shoppingCartDraft = await applicationDbContext.Orders.Include(o => o.CartItems)
                    .ThenInclude(ci => ci.Meal).ThenInclude(m => m.Price)
                    .FirstOrDefaultAsync(s => (s.User.UserName == userName && s.Restaurant == restaurant && s.OrderStatus == OrderStatus.Draft));
                if (shoppingCartDraft == null)
                {
                    shoppingCartDraft = new Order()
                    {
                        DateCreated = DateTime.UtcNow,
                        LastUpdate = DateTime.UtcNow,
                        User = user,
                        Restaurant = restaurant,
                        OrderStatus = OrderStatus.Draft
                    };
                    await applicationDbContext.Orders.AddAsync(shoppingCartDraft);
                    await applicationDbContext.SaveChangesAsync();
                }
                return shoppingCartDraft;
            }
            return null;
        }

        public async Task<ShoppingCartRequest> CreateShoppingCartRequestByUserAndRestaurantAsync(string userName, Address address, long restaurantId)
        {
            var order = await GetShoppingCartByUserAndRestaurantAsync(userName, restaurantId);
            if (order != null)
            {
                var shoppingCartRequest = mapper.Map<Order, ShoppingCartRequest>(order);
                if(address != null)
                {
                    shoppingCartRequest.Address = address;
                }
                return shoppingCartRequest;
            }
            return null;
        }

        public async Task<ShoppingCartRequest> CreateShoppingCartRequestByIdAsync(Address address, long orderId)
        {
            var order = await GetOrderById(orderId);
            if (order != null)
            {
                var shoppingCartRequest = mapper.Map<Order, ShoppingCartRequest>(order);
                if (address != null)
                {
                    shoppingCartRequest.Address = address;
                }
                return shoppingCartRequest;
            }
            return null;
        }

        public async Task<bool> ValidateAccessAsync(long cartItemId, string userName)
        {
            var cartItem = await GetCartItemByIdAsync(cartItemId);
            if(cartItem != null)
            {
                return cartItem.Order.User.UserName == userName;
            }
            return false;
        }

        public async Task<CartItem> GetCartItemByIdAsync(long cartItemId)
        {
            return await applicationDbContext.CartItems.Include(ci => ci.Order).ThenInclude(o => o.User).Include(ci => ci.Order).ThenInclude(o => o.Restaurant)
                .FirstOrDefaultAsync(ci => (ci.CartItemId == cartItemId));
        }

        public async Task DeleteCartItemAsync(long cartItemId)
        {
            var cartItem = await GetCartItemByIdAsync(cartItemId);
            if (cartItem != null)
            {
                applicationDbContext.CartItems.Remove(cartItem);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        public async Task SaveOrderAsync(long orderId, Address address)
        {
            var order = await GetOrderById(orderId);
            if(order != null)
            {
                order.Address = address;
                order.OrderStatus = OrderStatus.Ordered;
                order.DateSubmitted = DateTime.UtcNow;
            }
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<Order> GetOrderById(long orderId)
        {
            return await applicationDbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<int> GetNumberOfItemsInBasket(string userName, long restaurantId)
        {
            var shoppingCart = await GetShoppingCartByUserAndRestaurantAsync(userName, restaurantId);
            try
            {
                return shoppingCart.CartItems.Count;
            }
            catch
            {
                return 0;
            }
        }
        
        public async Task<List<Order>> GetOrdersByManagerAsync(ClaimsPrincipal user)
        {
            var currentOrders = await applicationDbContext.Orders.Where(or => or.OrderStatus == OrderStatus.Ordered).Where(or => or.Restaurant.Manager.Email == user.Identity.Name).Include(o => o.CartItems).ThenInclude(o => o.Meal).ThenInclude(o => o.Restaurant).ToListAsync();
            return currentOrders;
        }
    }
}
