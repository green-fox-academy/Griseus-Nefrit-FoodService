﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using Microsoft.AspNetCore.Mvc;
using FoodService.Services;

namespace FoodService.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService restaurantService;
        private readonly UserManager<AppUser> userMgr;

        public RestaurantController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(long id, RestaurantRequest restaurantReq)
        {
            var currentUser = await userMgr.GetUserAsync(HttpContext.User);
            if (!currentUser.IsInRole("Managers"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if(ModelState.IsValid)
            {
                await restaurantService.SaveRestaurantAsync(restaurantReq, id);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }
    }
}