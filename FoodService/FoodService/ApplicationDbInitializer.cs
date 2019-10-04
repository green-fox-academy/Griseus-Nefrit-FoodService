using FoodService.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService
{
    public class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<AppUser> userManager)
        {
            if (userManager.FindByEmailAsync("nefrit@gmail.com").Result == null)
            {
                AppUser user = new AppUser
                {
                    UserName = "nefrit@gmail.com",
                    Email = "nefrit@gmail.com"
                };
                IdentityResult result = userManager.CreateAsync(user, "Alma12").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}