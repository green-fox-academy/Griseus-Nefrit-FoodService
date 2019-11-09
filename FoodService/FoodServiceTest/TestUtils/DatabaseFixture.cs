using FoodService;
using FoodService.Models;
using FoodService.Models.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodServiceTest.TestUtils
{
    public class DatabaseFixture : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> options;
        public DatabaseFixture()
        {
            options = TestDbOptions.Get();
            using (var context = new ApplicationDbContext(options))
            {
                SeedRestaurants(context);
                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.Restaurants.RemoveRange(context.Restaurants);
                context.SaveChanges();
            }
        }

        private void SeedRestaurants(ApplicationDbContext context)
        {
            AppUser manager = new AppUser
            {
                UserName = "TestName",
                Id = "123",
                
            };

            context.Users.Add(manager);
            
            context.Orders.AddRange(new List<Order>()
            {
                new Order()
                {
                    OrderId = 5,
                    User = manager,
                    Restaurant = new Restaurant()
                    {
                        RestaurantId = 5
                    }
                }
            });
            
            Restaurant restaurant = new Restaurant
            {
                Name = "Test1",
                Manager = manager
            };
            Restaurant restaurant2 = new Restaurant
            {
                Name = "Test2",
                Manager = manager
            };

            context.Restaurants.AddRange(new List<Restaurant>
            {
                restaurant, restaurant2,
            });
            
            context.CartItems.AddRange(new List<CartItem>
            {
                new CartItem()
                {
                    CartItemId = 1,
                    Order = new Order()
                    {
                        OrderId = 5
                    },
                    Meal = new Meal()
                    {
                        MealId = 5
                    },
                    Quantity = 2
                }
            });

            context.Meals.AddRange(new List<Meal>
            {
                new Meal {
                    Name = "TestMeal",
                    Restaurant = restaurant
                },
                new Meal {
                    Name = "TestMeal2",
                    Restaurant = restaurant
                },
                new Meal {
                    Name = "TestMeal3",
                    Restaurant = restaurant2
                }
            });
        }
    }
}
