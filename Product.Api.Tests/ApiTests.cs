using System.Net;

namespace Product.Api.Tests
{
    public class ApiTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _factory;

        public ApiTests(ApiTestFixture factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetApiEndPoint_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/values");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}