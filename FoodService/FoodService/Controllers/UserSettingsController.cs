using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace FoodService.Controllers
{
    public class UserSettingsController : Controller
    {
        private readonly IStringLocalizer<UserSettingsController> localizer;

        public UserSettingsController(IStringLocalizer<UserSettingsController> localizer)
        {
            this.localizer = localizer;
        }

        [Authorize(Roles = "Customer, Manager, Admin")]
        [HttpGet]
        public IActionResult UserSettings()
        {
            return View();
        }

        [Authorize(Roles = "Customer, Manager, Admin")]
        [HttpGet]
        public IActionResult About()
        {
            ViewData["Message"] = localizer["Description"];
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }
    }
}