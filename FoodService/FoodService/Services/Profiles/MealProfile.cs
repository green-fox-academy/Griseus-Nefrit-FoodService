using AutoMapper;
using FoodService.Models;
using FoodService.Models.RequestModels.RestaurantRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.Profiles
{
    public class MealProfile : Profile
    {
        public MealProfile()
        {
            CreateMap<AddMealRequest, Meal>();
            CreateMap<Meal, AddMealRequest>();
        }
    }
}
