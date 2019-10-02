using AutoMapper;
using FoodService.Models.Identity;
using FoodService.Models.RequestModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterRequest, AppUser>()
                .ForMember(destination => destination.UserName,
                opts => opts.MapFrom(source => source.Email));
        }
    }
}
