using FoodService.Models.RequestModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.User
{
    public interface IUserService
    {
        Task<List<string>> LoginAsync(LoginRequest model);
        Task Logout();
        Task<List<string>> RegisterAsync(RegisterRequest model);
    }
}
