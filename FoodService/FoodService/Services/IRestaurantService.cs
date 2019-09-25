using FoodService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services
{
    public interface IRestaurantService
    {
        Task <List<Restaurant>> findAll();
    }
}
