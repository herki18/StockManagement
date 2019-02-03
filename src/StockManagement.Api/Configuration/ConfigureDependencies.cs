using Microsoft.Extensions.DependencyInjection;
using StockManagement.Api.BLL;
using StockManagement.Api.Contract.Interfaces.Repositories;
using StockManagement.Api.Contract.Interfaces.Services;
using StockManagement.Api.DAL.Repositories;

namespace StockManagement.Api.Configuration
{
    public class ConfigureDependencies
    {
        public void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IBatchesRepository, BatchesRepository>();
            services.AddScoped<IFruitsRepository, FruitsRepository>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IStocksService, StocksService>();
            services.AddScoped<IBatchesService, BatchesService>();
            services.AddScoped<IFruitsService, FruitsService>();
        }
    }
}