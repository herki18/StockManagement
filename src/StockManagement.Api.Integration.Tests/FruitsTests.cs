using System.Net;
using FluentAssertions;
using StockManagement.Api.Integration.Tests.Helpers;
using Xunit;

namespace StockManagement.Api.Integration.Tests
{
    public class FruitsTests
    {
        [Fact]
        public async void Should_Return_All_Fruits()
        {
            using (var client = new TestClientProvider<TestStartup>().Client)
            {
                var response = await client.GetAsync("/api/fruits");
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}