using System;
using System.Threading.Tasks;
using FoodService.Services.TimezoneService;
using FoodService.Services.User;
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
        private readonly ITimezoneService timezoneService;
        private readonly IUserService userService;
        public UserSettingsController(IStringLocalizer<UserSettingsController> localizer, ITimezoneService timezoneService, IUserService userService)
        {
            this.localizer = localizer;
            this.timezoneService = timezoneService;
            this.userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UserSettings()
        {
            string username = User.Identity.Name;
            var items = timezoneService.GetTimezones();
            string timezone = await timezoneService.GetTimezoneAsync(User.Identity.Name);
            ViewBag.TimeZones = items;
            ViewBag.Userstimezone = timezone;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetLanguage(string culture, string returnUrl, string timeZoneId)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            string username = User.Identity.Name;
            await timezoneService.SetUsersTimezoneAsync(username, timeZoneId);
            return LocalRedirect(returnUrl);
        }
    }
}