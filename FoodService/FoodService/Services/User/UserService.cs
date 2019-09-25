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

        public async Task<SignInResult> LoginAsync(LoginRequest loginRequest)
        {
            var result = await signInMgr.PasswordSignInAsync(userName: loginRequest.Email,
                loginRequest.Password, isPersistent: false, lockoutOnFailure: false);
            return result;
        }

        public async Task Logout()
        {
            await signInMgr.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterAsync(RegisterRequest regRequest)
        {
            var user = new AppUser
            {
                UserName = regRequest.Email,
                Email = regRequest.Email
            };

            var result = await userMgr.CreateAsync(user, regRequest.Password);
            if (result.Succeeded)
            {
                await signInMgr.SignInAsync(user, isPersistent: false);
            }

            return result;
        }
    }
}
