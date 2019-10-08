using AutoMapper;
using FoodService.Models;
using FoodService.Models.RequestModels.OrderRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<ShoppingCartRequest, Order>()
                .ForMember(destination => destination.OrderId,
                opts => opts.MapFrom(source => source.ShoppingCartId));
            CreateMap<Order, ShoppingCartRequest>()
                .ForMember(destination => destination.ShoppingCartId,
                opts => opts.MapFrom(source => source.OrderId));
        }
    }
}
