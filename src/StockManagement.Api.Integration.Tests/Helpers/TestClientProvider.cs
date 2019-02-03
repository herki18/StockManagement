using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using StockManagement.Api.DAL;

namespace StockManagement.Api.Integration.Tests.Helpers
{
    public class TestClientProvider<T> : IDisposable where T : class
    {
        private readonly TestServer _server;

        public TestClientProvider()
        {
            var appRootPath = Path.GetFullPath(Path.Combine(
                PlatformServices.Default.Application.ApplicationBasePath
                , "..", "..", "..", "..", "..", "src", "StockManagement.Api"));

            var builder = WebHost.CreateDefaultBuilder()
                .UseStartup<T>()
                .UseSetting(WebHostDefaults.ApplicationKey, typeof(Program).GetTypeInfo().Assembly.FullName)
                .UseEnvironment("Development")
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    config.SetBasePath(appRootPath);
                    config.AddJsonFile("appsettings.json", false, true);
                })
                .ConfigureServices(
                    s =>
                    {
                        var inMemorySqlite = new SqliteConnection("Data Source=:memory:");
                        inMemorySqlite.Open();
                        s.AddDbContext<ApiContext>(opt => opt.UseSqlite(inMemorySqlite));
                    });

            _server = new TestServer(builder);

            Client = _server.CreateClient();
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            _server?.Dispose();
            Client?.Dispose();
        }
    }
}