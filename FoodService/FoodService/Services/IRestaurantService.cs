using System.Collections.Generic;
using FoodService.Models;

namespace FoodService.Services
{
    public interface IRestaurantService
    {
        Restaurant getRestaurantById(long id);
    }
}