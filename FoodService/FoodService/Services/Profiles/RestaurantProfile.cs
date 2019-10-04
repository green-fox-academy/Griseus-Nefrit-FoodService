using AutoMapper;
using FoodService.Models;
using FoodService.Models.RequestModels.RestaurantRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<RestaurantRequest, Restaurant>();
            CreateMap<Restaurant, RestaurantRequest>();
        }
    }
}
