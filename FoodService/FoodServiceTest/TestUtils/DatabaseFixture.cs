using FoodService;
using FoodService.Models;
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
            context.Restaurants.AddRange(new List<Restaurant>
            {
                new Restaurant { Name = "Test" },
            });
        }
    }
}
