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
        public IEnumerable<TimeZoneInfo> GetTimezones()
        {
            return TimeZoneInfo.GetSystemTimeZones();
        }
        public async Task SetUsersTimezone(string username, string timezone)
        {
            var user = await userService.FindUserByNameOrEmailAsync(username);
            user.TimezoneId = timezone;
            await applicationDbContext.SaveChangesAsync();
        }
        public async Task<string> GetTimezoneAsync(string username)
        {
            var user = await userService.FindUserByNameOrEmailAsync(username);
            return user.TimezoneId;
        }
    }
}
