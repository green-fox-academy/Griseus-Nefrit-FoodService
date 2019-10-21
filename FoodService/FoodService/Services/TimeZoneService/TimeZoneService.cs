using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodService.Services.User;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodService.Services.TimezoneService
{
    public class TimezoneService : ITimezoneService
    {
        private readonly IUserService userService;
        private readonly ApplicationDbContext applicationDbContext;

        public TimezoneService(IUserService userService, ApplicationDbContext applicationDbContext)
        {
            this.userService = userService;
            this.applicationDbContext = applicationDbContext;
        }

        public List<SelectListItem> GetTimezones()
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var timeZone in timeZones)
            {
                items.Add(new SelectListItem() { Text = timeZone.Id });
            }
            return items;
        }
        public void setUsersTimezone(string username, string timezone)
        {
            var user = userService.FindUserByNameOrEmailAsync(username).Result;
            user.Timezone = timezone;
            applicationDbContext.SaveChanges();
        }

        public string getTimezone(string username)
        {
            var user = userService.FindUserByNameOrEmailAsync(username).Result;
            return user.Timezone;
        }
    }
}
