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

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await userService.Logout();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginRequest loginRequest = await userService.CreateLoginRequest(returnUrl);
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
                loginRequest = await userService.CreateLoginRequest(String.Empty);
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
            try
            {
                returnUrl = returnUrl ?? Url.Content("~/");
                string succeededReturnUrl = await userService.ExternalLoginCallbackAsync(returnUrl, remoteError);
                return LocalRedirect(succeededReturnUrl);
            }
            catch (InvalidOperationException ex)
            {
                LoginRequest loginRequest = await userService.CreateLoginRequest(returnUrl);
                ModelState.AddModelError(string.Empty, ex.Message);
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
