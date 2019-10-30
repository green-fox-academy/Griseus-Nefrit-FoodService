using AutoMapper;
using FoodService;
using Microsoft.EntityFrameworkCore;
using Moq;
using FoodService.Services.BlobService;
using Xunit;
using FoodServiceTest.TestUtils;
using System.Threading.Tasks;
using FoodService.Models.RequestModels.RestaurantRequestModels;
using FoodService.Models;
using FoodService.Services.RestaurantService;
using FoodService.Services.MealService;

namespace FoodServiceTest.Services
{
    [Collection("Database collection")]
    public class MealServiceTests
    {
        private readonly DbContextOptions<ApplicationDbContext> options;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IBlobStorageService> mockBlobStorageService;
        private readonly Mock<IRestaurantService> mockRestaurantService;

        public MealServiceTests()
        {
            options = TestDbOptions.Get();
            mockMapper = new Mock<IMapper>();
            mockBlobStorageService = new Mock<IBlobStorageService>();
            mockRestaurantService = new Mock<IRestaurantService>();
        }

        [Fact]
        public async Task Meal_is_added_when_correct()
        {
            using (var context = new ApplicationDbContext(options))
            {
                var mealRequest = new AddMealRequest()
                {
                    Name = "Test",
                    RestaurantId = 1,
                };

                mockMapper.Setup(x => x.Map<AddMealRequest, Meal>(It.IsAny<AddMealRequest>()))
                    .Returns(new Meal()
                    {
                        Name = mealRequest.Name,
                        
                    });

                var mealService = new MealService(context, mockMapper.Object, mockBlobStorageService.Object, mockRestaurantService.Object);
                var length = await context.Meals.CountAsync();
                await mealService.SaveMealAsync(mealRequest);
                Assert.Equal(length + 1, await context.Meals.CountAsync());
            }
        }
    }
}
