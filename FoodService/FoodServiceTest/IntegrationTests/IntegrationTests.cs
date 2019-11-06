using FoodService;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FoodServiceTest.IntegrationTests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Account/Login")]
        [InlineData("/Meal/Add")]
        [InlineData("/Order/History")]
        public async Task MainPages_Load_Successfully(string url)
        {
            var responseMessage = await factory.CreateClient().GetAsync(url);

            responseMessage.EnsureSuccessStatusCode();
        }
    }
}
