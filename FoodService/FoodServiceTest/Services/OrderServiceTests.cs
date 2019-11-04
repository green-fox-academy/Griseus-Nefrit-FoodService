using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoodService;
using FoodService.Models;
using FoodService.Models.RequestModels.OrderRequestModels;
using FoodService.Services.BlobService;
using FoodService.Services.EmailService;
using FoodService.Services.MealService;
using FoodService.Services.OrderService;
using FoodService.Services.RestaurantService;
using FoodService.Services.User;
using FoodServiceTest.TestUtils;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace FoodServiceTest.Services
{
    [Collection("Database collection")]
    public class OrderServiceTests
    {
        private readonly DbContextOptions<ApplicationDbContext> options;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IUserService> mockUserService;
        private readonly Mock<IMealService> mockMealService;
        private readonly Mock<IRestaurantService> mockRestaurantService;
        private readonly Mock<IEmailService> mockEmailService;
        
        public OrderServiceTests()
        {
            options = TestDbOptions.Get();
            mockMapper = new Mock<IMapper>();
            mockUserService = new Mock<IUserService>();
            mockMealService = new Mock<IMealService>();
            mockRestaurantService = new Mock<IRestaurantService>();
            mockEmailService = new Mock<IEmailService>();
        }

        [Fact]
        public async Task Meal_Is_Added_To_ShoppingCart()
        {
            using (var context = new ApplicationDbContext(options))
            {
                
            }
        }

        [Fact]
        public async Task CartItem_Is_Deleted_From_ShoppingCart()
        {
            using (var context = new ApplicationDbContext(options))
            {
                var orderService = new OrderService(context, mockUserService.Object, mockMealService.Object, mockRestaurantService.Object,mockEmailService.Object, mockMapper.Object);
                var length = await context.CartItems.CountAsync();
                await orderService.DeleteCartItemAsync(1L);
                
                Assert.Equal(length - 1, await context.CartItems.CountAsync());
            }
        } 
    }
}