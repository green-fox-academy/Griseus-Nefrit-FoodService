using FoodService.Models.Identity;
using FoodService.Models.RequestModels.Account;
using FoodService.Services.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodService.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await userService.Logout();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginRequest loginRequest = new LoginRequest
            {
                ReturnUrl = returnUrl,
                ExternalLogins = await userService.GetExternalAuthenticationSchemesAsync()
            };

            return View(loginRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await userService.LoginAsync(loginRequest);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Email or Password");
            }
            return View(loginRequest);
        }

        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = userService.ConfigureExternalAuthenticaticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginRequest loginRequest = new LoginRequest
            {
                ReturnUrl = returnUrl,
                ExternalLogins = await userService.GetExternalAuthenticationSchemesAsync()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("Login", loginRequest);
            }

            var userLoginInfo = await userService.GetExternalLoginInfoAsync();
            if (userLoginInfo == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");
                return View("Login", loginRequest);
            }

            var signInResult = await userService.ExternalLoginSignInAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = userLoginInfo.Principal.FindFirst(ClaimTypes.Email).Value;

                if (email != null)
                {
                    await userService.RegisterExternalUserAsync(email, userLoginInfo);
                    return LocalRedirect(returnUrl);
                }

                ModelState.AddModelError(string.Empty, $"Email claim not received from: {userLoginInfo.LoginProvider}");
                return View("Login", loginRequest);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest regRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.RegisterAsync(regRequest);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(regRequest);
        }
    }
}
