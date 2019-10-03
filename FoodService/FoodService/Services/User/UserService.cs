using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodService.Models.Identity;
using FoodService.Models.RequestModels.Account;
using Microsoft.AspNetCore.Identity;

namespace FoodService.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userMgr;
        private readonly SignInManager<AppUser> signInMgr;
        private readonly IMapper iMapper;

        public UserService(UserManager<AppUser> userMgr, SignInManager<AppUser> signInMgr, IMapper iMapper)
        {
            this.userMgr = userMgr;
            this.signInMgr = signInMgr;
            this.iMapper = iMapper;
        }

        public async Task<AppUser> FindUserByNameOrEmail(string nameOrEmailAddr)
        {
            return await userMgr.FindByEmailAsync(nameOrEmailAddr);
        }

        public async Task<SignInResult> LoginAsync(LoginRequest loginRequest)
        {
            var result = await signInMgr.PasswordSignInAsync(userName: loginRequest.Email, password: loginRequest.Password, isPersistent: false, lockoutOnFailure: false);
            return result;
        }

        public async Task Logout()
        {
            await signInMgr.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterAsync(RegisterRequest regRequest)
        {
            var user = iMapper.Map<RegisterRequest, AppUser>(regRequest);
            var result = await userMgr.CreateAsync(user, regRequest.Password);
            if (result.Succeeded)
            {
                await userMgr.AddToRoleAsync(user, regRequest.Manager ? "Manager" : "Customer");
                await signInMgr.SignInAsync(user, isPersistent: false);
            }

            return result;
        }
    }
}
