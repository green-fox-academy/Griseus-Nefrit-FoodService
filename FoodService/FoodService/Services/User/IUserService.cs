using FoodService.Models.RequestModels.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.User
{
    public interface IUserService
    {
        Task<List<string>> LoginAsync(LoginRequest loginRequest);
        Task Logout();
        Task<IdentityResult> RegisterAsync(RegisterRequest regRequest);
    }
}
