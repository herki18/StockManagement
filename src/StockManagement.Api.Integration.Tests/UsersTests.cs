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
    public class UsersTests
    {
        [Fact]
        public async Task Should_Authenicate_The_User()
        {
            using (var client = new TestClientProvider<Startup>().Client)
            {
                var json = JsonConvert.SerializeObject(new UserDTO
                {
                    Username = SeedData.FirstUser.Username,
                    Password = "Password"
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/users/authenticate", content);
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task Should_Return_BadRequest_When_Password_Is_Wrong()
        {
            using (var client = new TestClientProvider<Startup>().Client)
            {
                var json = JsonConvert.SerializeObject(new UserDTO
                {
                    Username = SeedData.FirstUser.Username,
                    Password = "Wrong"
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/users/authenticate", content);
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }

        [Fact]
        public async Task Should_Return_BadRequest_When_UserName_Is_Wrong()
        {
            using (var client = new TestClientProvider<Startup>().Client)
            {
                var json = JsonConvert.SerializeObject(new UserDTO
                {
                    Username = "Wrong",
                    Password = "Wrong"
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/users/authenticate", content);
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }
    }
}