using AutoMapper;
using FoodService;
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

        public OrderServiceTests()
        {
            options = TestDbOptions.Get();
            mockMapper = new Mock<IMapper>();
        }
    }
}