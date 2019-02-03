using System;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackifyMiddleware;
using StockManagement.Api.Configuration;
using StockManagement.Api.Contract;
using StockManagement.Api.Contract.Helpers;
using StockManagement.Api.DAL;

namespace StockManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            services.AddDbContext<ApiContext>(opt => opt.UseSqlite("Data Source=StockManagement.db;"));

            // Validation rules

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                {
                    mc.CreateMissingTypeMaps = false;
                    mc.AddProfile(new MappingProfiles());
                }
            });

            // Set Adapter pattern
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            ConfigureJwt(services);

            var ded = new ConfigureDependencies();
            ded.ConfigureRepositories(services);
            ded.ConfigureServices(services);

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "StockManagement API",
                    Description = "StockManagement API"
                });
            });

            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.SetIsOriginAllowed(host => true);
            corsBuilder.AllowCredentials();

            services.AddCors(options => { options.AddPolicy("SiteCorsPolicy", corsBuilder.Build()); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApiContext apiContext)
        {
            app.UseCors("SiteCorsPolicy");

            if (env.IsDevelopment())
            {
                apiContext.Database.Migrate();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                apiContext.Database.Migrate();
            }

            app.UseAuthentication();

            app.UseSwagger();
            app.UseMiddleware<RequestTracerMiddleware>();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "api";
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        protected virtual void ConfigureJwt(IServiceCollection services)
        {
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.IncludeErrorDetails = true;
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });
        }
    }
}