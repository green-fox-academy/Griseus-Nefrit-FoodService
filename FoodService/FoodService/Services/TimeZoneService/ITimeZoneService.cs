using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodService.Services.TimezoneService
{
    public interface ITimezoneService
    {
        IEnumerable<TimeZoneInfo> GetTimezones();
        Task <string> GetTimezoneAsync(string username);
        Task SetUsersTimezoneAsync(string username, string timezone);
    }
}
