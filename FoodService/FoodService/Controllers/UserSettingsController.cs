using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Services.TimezoneService;
using FoodService.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodService.Controllers
{
    public class UserSettingsController : Controller
    {
        private readonly ITimezoneService timezoneService;
        private readonly IUserService userService;
        public UserSettingsController(ITimezoneService timezoneService, IUserService userService)
        {
            this.timezoneService = timezoneService;
            this.userService = userService;
        }

        [HttpGet("User/Usersettings")]
        public async Task<IActionResult> Index()
        {
            string username = User.Identity.Name;
            var items = timezoneService.GetTimezones();
            string timezone = await timezoneService.GetTimezoneAsync(User.Identity.Name);
            ViewBag.TimeZones = items;
            ViewBag.Userstimezone = timezone;
            return View();
        }

        [HttpPost("User/Usersettings")]
        public async Task<IActionResult> SetTimezone(string timeZoneId)
        {
            string username = User.Identity.Name;
            await timezoneService.SetUsersTimezone(username, timeZoneId);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}