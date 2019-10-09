using AutoMapper;
using FoodService.Models;
using FoodService.Models.Identity;
using FoodService.Models.RequestModels.OrderRequestModels;
using FoodService.Services.MealService;
using FoodService.Services.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IUserService userService;
        private readonly IMealService mealService;
        private readonly IMapper iMapper;

        public OrderService(ApplicationDbContext applicationDbContext, IUserService userService, IMealService mealService, IMapper iMapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.userService = userService;
            this.mealService = mealService;
            this.iMapper = iMapper;
        }

        public async Task<Order> AddMealToOrderAsync(long mealId, string userName)
        {
            var shoppingCart = await GetShoppingCartByUserAsync(userName);
            if(shoppingCart != null)
            {
                var meal = await mealService.GetMealByIdAsync(mealId);
                if(meal != null)
                {
                    var cartItem = await applicationDbContext.CartItems.FirstOrDefaultAsync(c => c.Meal == meal && c.Order == shoppingCart);
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

        public async Task<Order> GetShoppingCartByUserAsync(string userName)
        {
            AppUser user = await userService.FindUserByNameOrEmail(userName);
            if (user != null)
            {
                var shoppingCartDraft = await applicationDbContext.Orders.Include(o => o.CartItems).ThenInclude(ci => ci.Meal).ThenInclude(m => m.Price).FirstOrDefaultAsync(s => (s.User.UserName == userName && s.OrderStatus == OrderStatus.Draft));
                if (shoppingCartDraft == null)
                {
                    shoppingCartDraft = new Order()
                    {
                        DateCreated = DateTime.Now,
                        LastUpdate = DateTime.Now,
                        User = user,
                        OrderStatus = OrderStatus.Draft
                    };
                    await applicationDbContext.Orders.AddAsync(shoppingCartDraft);
                    await applicationDbContext.SaveChangesAsync();
                }
                return shoppingCartDraft;
            }
            return null;
        }

        public async Task<ShoppingCartRequest> CreateShoppingCartRequestByUserAsync(string userName, Address address)
        {
            var order = await GetShoppingCartByUserAsync(userName);
            if (order != null)
            {
                var shoppingCartRequest = iMapper.Map<Order, ShoppingCartRequest>(order);
                if(address != null)
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
            return await applicationDbContext.CartItems.Include(ci => ci.Order).ThenInclude(o => o.User).FirstOrDefaultAsync(ci => (ci.CartItemId == cartItemId));
        }

        public async Task DeleteCartItemAsync(long cartItemId)
        {
            var cartItem = await GetCartItemByIdAsync(cartItemId);
            //var meal = await GetMealByIdAsync(id);
            if (cartItem != null)
            {
                applicationDbContext.CartItems.Remove(cartItem);
                applicationDbContext.SaveChanges();
            }
        }

        public async Task SaveOrderAsync(long orderId, Address address)
        {
            var order = await GetOrderById(orderId);
            if(order != null)
            {
                order.Address = address;
                order.OrderStatus = OrderStatus.Ordered;
            }
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<Order> GetOrderById(long orderId)
        {
            return await applicationDbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<int> GetNumberOfItemsInBasket(string userName)
        {
            var shoppingCart = await GetShoppingCartByUserAsync(userName);
            try
            {
                return shoppingCart.CartItems.Count;
            }
            catch
            {
                return 0;
            }
            
        }
    }
}
