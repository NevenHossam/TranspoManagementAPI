using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TranspoManagementAPI.Tests
{
    public class RateLimiterTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public RateLimiterTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ExceedingGlobalRateLimit_ShouldReturn429()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Test-Client", "TestClient1");

            int requestsToSend = 11; 

            HttpResponseMessage? response = null;

            // Act - Send over requests limits to the API endpoint
            for (int i = 0; i < requestsToSend; i++)
            {
                response = await client.GetAsync("/api/FareBand");
            }

            // Assert
            Assert.NotNull(response);
            Assert.Equal((HttpStatusCode)429, response.StatusCode);
        }

        [Fact]
        public async Task RequestsExactLimit_ShouldReturnSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Test-Client", "TestClient2");

            int limit = 10;
            HttpResponseMessage? response = null;

            // Act - Send the limit exactly
            for (int i = 0; i < limit; i++)
            {
                response = await client.GetAsync("/api/FareBand");
                Assert.True(response.IsSuccessStatusCode, $"Request {i + 1} should succeed");
            }
        }

        [Fact]
        public async Task RequestsWithinLimit_ShouldReturnSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Test-Client", "TestClient3");

            int limit = 3;
            HttpResponseMessage? response = null;

            // Act - Send the limit exactly
            for (int i = 0; i < limit; i++)
            {
                response = await client.GetAsync("/api/FareBand");
                Assert.True(response.IsSuccessStatusCode, $"Request {i + 1} should succeed");
            }
        }
    }
}
