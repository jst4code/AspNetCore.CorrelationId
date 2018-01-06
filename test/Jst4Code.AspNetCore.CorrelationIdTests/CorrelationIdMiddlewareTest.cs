using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace Jst4Code.AspNetCore.CorrelationIdTests
{
    public class CorrelationIdMiddlewareTest
    {
        [Fact]
        public async void CorrelationId_In_Response()
        {
            // Arrange
            var builder = new WebHostBuilder()
               .Configure(app => app.AddCorrelationId());
            TestServer server = new TestServer(builder);

            var client = server.CreateClient();

            // Act
            var response = await client.GetAsync("");

            // Assert
            Assert.True(!string.IsNullOrEmpty(response.Headers.GetValues("X-CorrelationId").FirstOrDefault()));
        }

        [Fact]
        public async void Same_CorrelationId_In_Response()
        {
            // Arrange
            var builder = new WebHostBuilder()
               .Configure(app => app.AddCorrelationId());
            TestServer server = new TestServer(builder);

            var client = server.CreateClient();

            // Act
            client.DefaultRequestHeaders.Add("X-CorrelationId", "12345");
            var response = await client.GetAsync("");

            // Assert
            Assert.Equal("12345", response.Headers.GetValues("X-CorrelationId").FirstOrDefault());
        }
    }
}
