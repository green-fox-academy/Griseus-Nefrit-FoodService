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
        List<SelectListItem> GetTimezones();
        string getTimezone(string username);
        void setUsersTimezone(string username, string timezone);
    }
}
