using FoodService.Models.Identity;
using FoodService.Models.RequestModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.User
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAsync(AppUser user);
        Task<SignInResult> LoginAsync(LoginRequest loginRequest);
        Task Logout();
        Task<IdentityResult> RegisterAsync(RegisterRequest regRequest);
        Task<AppUser> FindUserByNameOrEmailAsync(string emailAddr);
        Task<IList<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
        AuthenticationProperties ConfigureExternalAuthenticaticationProperties(string provider, string redirectUrl);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey);
        Task<SignInResult> RegisterExternalUserAsync(string emailAddr, ExternalLoginInfo userInfo);
    }
}
