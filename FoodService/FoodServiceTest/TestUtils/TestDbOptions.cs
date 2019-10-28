using FoodService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace FoodServiceTest.TestUtils
{
    class TestDbOptions
    {
        public static DbContextOptions<ApplicationDbContext> Get()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "foodservice-testdb")
                .Options;
        }
    }
}