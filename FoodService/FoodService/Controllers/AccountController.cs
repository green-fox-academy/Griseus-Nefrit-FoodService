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
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(regRequest);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await userService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
