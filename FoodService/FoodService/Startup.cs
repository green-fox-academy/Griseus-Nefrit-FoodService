using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Services;
using FoodService.Services.MealService;
using FoodService.Services.RestaurantService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FoodService.Models.Identity;
using FoodService.Services.User;
using Microsoft.AspNetCore.Identity;
using ReflectionIT.Mvc.Paging;
using FoodService.Services.BlobService;
using FoodService.Services.Profiles;


namespace FoodService
{
    public class Startup
    {
        private IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<ApplicationDbContext>(build =>
                {
                    build.UseMySql(configuration.GetConnectionString("AzureConnection"));
                });
                // Automatically perform database migration
                services.BuildServiceProvider().GetService<ApplicationDbContext>().Database.Migrate();
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(build =>
                {
                    build.UseMySql(configuration.GetConnectionString("DefaultConnection"));
                });
            }

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddTransient<IMealService, MealService>();
            services.AddTransient<IBlobStorageService, BlobStorageService>();
            services.SetUpAutoMapper();
            services.AddMvc();
            services.AddPaging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<AppUser> userManager)
        {
            ApplicationDbInitializer.SeedUsers(userManager);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}

