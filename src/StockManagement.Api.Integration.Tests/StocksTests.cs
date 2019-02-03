using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using StockManagement.Api.Contract.Models;
using StockManagement.Api.DAL;
using StockManagement.Api.Integration.Tests.Helpers;
using Xunit;

namespace StockManagement.Api.Integration.Tests
{
    public class StocksTests
    {
        [Fact]
        public async Task Should_Return_Fourth_Stock()
        {
            using (var client = new TestClientProvider<TestStartup>().Client)
            {
                var createBatch = new BatchForCreate
                {
                    FruitId = SeedData.RaspberryTestFruit.Id,
                    VarietyId = SeedData.RaspberryTestFruit.Variety.Id,
                    Quantity = 20
                };

                var json = JsonConvert.SerializeObject(createBatch);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responseCreate = await client.PostAsync("/api/batches", content);
                responseCreate.StatusCode.Should().Be(HttpStatusCode.Created);

                var response = await client.GetAsync("/api/stocks");
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var stocks = JsonConvert.DeserializeObject<List<StockDTO>>(response.Content.ReadAsStringAsync().Result);

                Assert.Collection(stocks,
                    dto => Assert.Equal(15, dto.Quantity),
                    dto => Assert.Equal(22, dto.Quantity),
                    dto => Assert.Equal(10, dto.Quantity),
                    dto => Assert.Equal(20, dto.Quantity));
            }
        }

        [Fact]
        public async Task Should_Return_Stocks()
        {
            using (var client = new TestClientProvider<TestStartup>().Client)
            {
                var response = await client.GetAsync("/api/stocks");
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var stocks = JsonConvert.DeserializeObject<List<StockDTO>>(response.Content.ReadAsStringAsync().Result);

                Assert.Collection(stocks,
                    dto => Assert.Equal(15, dto.Quantity),
                    dto => Assert.Equal(22, dto.Quantity),
                    dto => Assert.Equal(10, dto.Quantity));
            }
        }

        [Fact]
        public async void Should_Return_Two_Stocks_When_Removing_One_Batch()
        {
            using (var client = new TestClientProvider<TestStartup>().Client)
            {
                var responseDelete = await client.DeleteAsync($"/api/batches/{SeedData.SecondBatch.Id}");
                responseDelete.StatusCode.Should().Be(HttpStatusCode.OK);

                var response = await client.GetAsync("/api/stocks");
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var stocks = JsonConvert.DeserializeObject<List<StockDTO>>(response.Content.ReadAsStringAsync().Result);

                Assert.Collection(stocks,
                    dto => Assert.Equal(15, dto.Quantity),
                    dto => Assert.Equal(22, dto.Quantity));
            }
        }

        [Fact]
        public async void Should_Return_Updated_Stocks_When_Removing_One_Batch()
        {
            using (var client = new TestClientProvider<TestStartup>().Client)
            {
                var responseDelete = await client.DeleteAsync($"/api/batches/{SeedData.FirstBatch.Id}");
                responseDelete.StatusCode.Should().Be(HttpStatusCode.OK);

                var response = await client.GetAsync("/api/stocks");
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var stocks = JsonConvert.DeserializeObject<List<StockDTO>>(response.Content.ReadAsStringAsync().Result);

                Assert.Collection(stocks,
                    dto => Assert.Equal(15, dto.Quantity),
                    dto => Assert.Equal(10, dto.Quantity),
                    dto => Assert.Equal(10, dto.Quantity));
            }
        }
    }
}