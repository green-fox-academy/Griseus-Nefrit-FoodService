using FoodService.Models;
using FoodService.Models.Identity;
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

        public OrderService(ApplicationDbContext applicationDbContext, IUserService userService, IMealService mealService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userService = userService;
            this.mealService = mealService;
        }

        public async Task<ShoppingCart> AddMealToOrderAsync(long mealId, string userName)
        {
            var shoppingCart = await GetShoppingCartByUserAsync(userName);
            if(shoppingCart != null)
            {
                var meal = await mealService.GetMealByIdAsync(mealId);
                if(meal != null)
                {
                    var cartItem = await applicationDbContext.CartItems.FirstOrDefaultAsync(c => c.Meal == meal && c.ShoppingCart == shoppingCart);
                    if(cartItem == null)
                    {
                        var newCartItem = new CartItem()
                        {
                            Quantity = 1,
                            Meal = meal,
                            ShoppingCart = shoppingCart
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

        public async Task<ShoppingCart> GetShoppingCartByUserAsync(string userName)
        {
            AppUser user = await userService.FindUserByNameOrEmail(userName);
            if (user != null)
            {
                var shoppingCartDraft = await applicationDbContext.ShoppingCarts.FirstOrDefaultAsync(s => (s.User.UserName == userName && s.OrderStatus == OrderStatus.Draft));
                if (shoppingCartDraft == null)
                {
                    shoppingCartDraft = new ShoppingCart()
                    {
                        DateCreated = DateTime.Now,
                        LastUpdate = DateTime.Now,
                        User = user,
                        OrderStatus = OrderStatus.Draft
                    };
                    await applicationDbContext.ShoppingCarts.AddAsync(shoppingCartDraft);
                    await applicationDbContext.SaveChangesAsync();
                }
                return shoppingCartDraft;
            }
            return null;
        }
    }
}
