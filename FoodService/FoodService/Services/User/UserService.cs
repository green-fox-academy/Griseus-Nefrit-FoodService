using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models.Identity;
using FoodService.Models.RequestModels.Account;
using Microsoft.AspNetCore.Identity;

namespace FoodService.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userMgr;
        private readonly SignInManager<AppUser> signInMgr;

        public UserService(UserManager<AppUser> userMgr, SignInManager<AppUser> signInMgr)
        {
            this.userMgr = userMgr;
            this.signInMgr = signInMgr;
        }

        public async Task<List<string>> LoginAsync(LoginRequest model)
        {
            throw new NotImplementedException();
        }

        public async Task Logout()
        {
            await signInMgr.SignOutAsync();
        }

        public async Task<List<string>> RegisterAsync(RegisterRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
