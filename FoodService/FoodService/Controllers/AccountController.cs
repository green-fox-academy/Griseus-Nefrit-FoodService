using FoodService.Models.Identity;
using FoodService.Models.RequestModels.Account;
using FoodService.Services.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var result = await userService.LoginAsync(loginRequest);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Email or Password");
            }
            return View(loginRequest);
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
