using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using StockManagement.Api.Contract.Models;
using StockManagement.Api.Contract.Models.Batch;
using StockManagement.Api.DAL;
using StockManagement.Api.Integration.Tests.Helpers;
using Xunit;

namespace StockManagement.Api.Integration.Tests
{
    public class BatchesTests
    {
        [Fact]
        public async Task Should_Create_New_Batch()
        {
            using (var client = new TestClientProvider<TestStartup>().Client)
            {
                var createBatch = new BatchForCreate
                {
                    FruitId = SeedData.RaspberryAmiraFruit.Id,
                    VarietyId = SeedData.AmiraVariety.Id,
                    Quantity = 20
                };

                var json = JsonConvert.SerializeObject(createBatch);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/batches", content);
                response.StatusCode.Should().Be(HttpStatusCode.Created);

                var batch = JsonConvert.DeserializeObject<BatchDTO>(response.Content.ReadAsStringAsync().Result);

                Assert.Equal(createBatch.FruitId, batch.FruitId);
                Assert.Equal(createBatch.VarietyId, batch.VarietyId);
                Assert.Equal(createBatch.Quantity, batch.Quantity);
            }
        }

        [Fact]
        public async void Should_Delete_Existing_Batch()
        {
            using (var client = new TestClientProvider<TestStartup>().Client)
            {
                var response = await client.DeleteAsync($"/api/batches/{SeedData.FirstBatch.Id}");
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var testResponse = await client.GetAsync($"/api/batches/{SeedData.FirstBatch.Id}");
                testResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
            }
        }

        [Fact]
        public async Task Should_Get_All_Batches()
        {
            using (var client = new TestClientProvider<TestStartup>().Client)
            {
                var response = await client.GetAsync("/api/batches");
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var batches = JsonConvert.DeserializeObject<List<BatchDTO>>(response.Content.ReadAsStringAsync().Result);

                Assert.Equal(4, batches.Count);
                Assert.Contains(batches, dto => dto.Id == SeedData.FirstBatch.Id);
                Assert.Contains(batches, dto => dto.Id == SeedData.SecondBatch.Id);
                Assert.Contains(batches, dto => dto.Id == SeedData.ThirdBatch.Id);
                Assert.Contains(batches, dto => dto.Id == SeedData.FourthBatch.Id);
            }
        }

        [Fact]
        public async Task Should_Get_One_Batches_By_Id()
        {
            using (var client = new TestClientProvider<TestStartup>().Client)
            {
                var response = await client.GetAsync($"/api/batches/{SeedData.FirstBatch.Id}");
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var batch = JsonConvert.DeserializeObject<BatchDTO>(response.Content.ReadAsStringAsync().Result);

                Assert.Equal(SeedData.FirstBatch.Id, batch.Id);
                Assert.Equal(SeedData.FirstBatch.Fruit.Id, batch.FruitId);
                Assert.Equal(SeedData.FirstBatch.Fruit.Variety.Id, batch.VarietyId);
                Assert.Equal(SeedData.FirstBatch.Quantity, batch.Quantity);
            }
        }

        [Fact]
        public async void Should_Update_Existing_Batch()
        {
            using (var client = new TestClientProvider<TestStartup>().Client)
            {
                var updateBatch = new BatchForUpdate
                {
                    FruitId = SeedData.RaspberryErikaFruit.Id,
                    VarietyId = SeedData.RaspberryErikaFruit.Variety.Id,
                    Quantity = 25
                };

                var json = JsonConvert.SerializeObject(updateBatch);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"/api/batches/{SeedData.FirstBatch.Id}", content);
                response.StatusCode.Should().Be(HttpStatusCode.Created);

                var batch = JsonConvert.DeserializeObject<BatchDTO>(response.Content.ReadAsStringAsync().Result);

                Assert.Equal(SeedData.FirstBatch.Id, batch.Id);
                Assert.Equal(updateBatch.FruitId, batch.FruitId);
                Assert.Equal(updateBatch.VarietyId, batch.VarietyId);
                Assert.Equal(updateBatch.Quantity, batch.Quantity);
            }
        }
    }
}