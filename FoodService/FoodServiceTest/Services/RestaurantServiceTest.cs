using AutoMapper;
using FoodService;
using FoodService.Models;
using FoodService.Models.Identity;
using FoodService.Models.RequestModels.RestaurantRequestModels;
using FoodService.Services.BlobService;
using FoodService.Services.RestaurantService;
using FoodService.Services.User;
using FoodServiceTest.TestUtils;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FoodServiceTest.Services
{
    [Collection("Database collection")]
    public class RestaurantServiceTests
    {
        private readonly DbContextOptions<ApplicationDbContext> options;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IUserService> mockUserService;
        private readonly Mock<IBlobStorageService> mockBlobStorageService;

        public RestaurantServiceTests()
        {
            options = TestDbOptions.Get();
            mockMapper = new Mock<IMapper>();
            mockUserService = new Mock<IUserService>();
            mockBlobStorageService = new Mock<IBlobStorageService>();
        }

        [Fact]
        public async Task Restaurant_Is_Added_When_Correct()
        {
            using (var context = new ApplicationDbContext(options))
            {
                var restaurantRequest = new RestaurantRequest()
                {
                    Name = "Test"
                };

                var manager = new AppUser()
                {
                    UserName = "TestName2"
                };

                mockMapper.Setup(x => x.Map<RestaurantRequest, Restaurant>(It.IsAny<RestaurantRequest>()))
                .Returns(new Restaurant()
                {
                    Name = restaurantRequest.Name,
                    Manager = manager
                });

                mockUserService.Setup(x => x.FindUserByNameOrEmailAsync(manager.UserName)).Returns(Task.FromResult(manager));

                var restaurantService = new RestaurantService(context, mockUserService.Object, mockMapper.Object, mockBlobStorageService.Object);
                var length = await context.Restaurants.CountAsync();
                var restaurant = await restaurantService.SaveRestaurantAsync(restaurantRequest, manager.UserName);

                Assert.Equal(length + 1, await context.Restaurants.CountAsync());
                Assert.Equal(restaurant.Name, restaurantRequest.Name);
            }
        }

        [Fact]
        public async Task Restaurant_Is_Queried_By_Id()
        {
            using (var context = new ApplicationDbContext(options))
            {
                var restaurantService = new RestaurantService(context, mockUserService.Object, mockMapper.Object, mockBlobStorageService.Object);
                var length = await context.Restaurants.CountAsync();
                var restaurant = await restaurantService.GetRestaurantByIdAsync(2L);

                Assert.Equal(2, await context.Restaurants.CountAsync());
                Assert.Equal("Test2", restaurant.Name);
            }
        }
    }
}
