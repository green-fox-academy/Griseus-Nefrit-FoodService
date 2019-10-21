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


        [HttpGet("user/usersettings")]
        public async Task<IActionResult> Index()
        {
            string username = User.Identity.Name;
            List<SelectListItem> items = timezoneService.GetTimezones();
            string timezone = timezoneService.getTimezone(User.Identity.Name);
            ViewBag.TimeZones = items;
            ViewBag.Userstimezone = timezone;
            return View();
        }

        [HttpPost("user/usersettings")]
        public async Task<IActionResult> SetTimezone(string timezone)
        {
            string username = User.Identity.Name;
            timezoneService.setUsersTimezone(username, timezone);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
