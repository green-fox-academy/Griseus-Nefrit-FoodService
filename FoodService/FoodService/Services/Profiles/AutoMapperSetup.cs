using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.Profiles
{
    public static class AutoMapperSetup
    {
        public static void SetUpAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RestaurantProfile());
                cfg.AddProfile(new AccountProfile());
                cfg.AddProfile(new MealProfile());
            });
            IMapper iMapper = config.CreateMapper();
            services.AddSingleton(iMapper);
        }
    }
}
