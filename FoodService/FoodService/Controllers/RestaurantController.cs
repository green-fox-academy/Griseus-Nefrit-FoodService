using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodService.Controllers
{
    public class RestaurantController : Controller
    {

        private readonly IAuthorService authorService;

        [HttpPost("/AddRestaurant")]
        public IActionResult AddRestaurant(Restaurant newRestaurant)
        {
            //itt kéne valahogy az adatbázisba egy új ID-vel berakni
            return View();
        }

        [HttpGet("/home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}