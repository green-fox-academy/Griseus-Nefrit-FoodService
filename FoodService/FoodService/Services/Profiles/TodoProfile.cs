using AutoMapper;
using FoodService.Models;
using FoodService.Models.RequestModels.TodoRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.Profiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<TodoRequest, Todo>();
            CreateMap<Todo, TodoRequest>();
        }
    }
}
