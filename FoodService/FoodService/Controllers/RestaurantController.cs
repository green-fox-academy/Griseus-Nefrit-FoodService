using System.Threading.Tasks;
using FoodService.Models.RequestModels.RestaurantRequestModels;
using FoodService.Services.RestaurantService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FoodService.Services.BlobService;
using FoodService.Models.ViewModels;
using FoodService.Models.ViewModels.RestaurantViewModels;
using FoodService.Services.OrderService;
using FoodService.Services.User;
using FoodService.Models;

namespace FoodService.Controllers
{
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService restaurantService;
        private readonly IOrderService orderService;
        private readonly IUserService userService;

        public RestaurantController(IRestaurantService restaurantService, IOrderService orderService, IUserService userService)
        {
            this.restaurantService = restaurantService;
            this.orderService = orderService;
            this.userService = userService;
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(RestaurantRequest restaurantRequest)
        {
            if (ModelState.IsValid)
            {
                await restaurantService.SaveRestaurantAsync(restaurantRequest, User.Identity.Name);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var editRestaurantViewModel = new EditRestaurantViewModel()
            {
                RestaurantRequest = restaurantRequest,
                Meals = null,
                RestaurantId = 0
            };
            return View(editRestaurantViewModel);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            if (!await restaurantService.ValidateAccessAsync(id, User))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var editRestaurantViewModel = await restaurantService.BuildEditRestaurantViewModelAsync(id);
            return View(editRestaurantViewModel);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditRestaurantViewModel editRestaurantViewModel, long id)
        {
            if (!await restaurantService.ValidateAccessAsync(id, User))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (ModelState.IsValid)
            {
                await restaurantService.EditRestaurantAsync(id, editRestaurantViewModel.RestaurantRequest);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            editRestaurantViewModel = await restaurantService.BuildEditRestaurantViewModelAsync(id, editRestaurantViewModel.RestaurantRequest);
            return View(editRestaurantViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index([FromRoute] long id)
        {
            var restaurant = await restaurantService.GetRestaurantByIdAsync(id);
            int numberOfCartItems = 0;
            if (User.Identity.Name != null)
            {
                numberOfCartItems = await orderService.GetNumberOfItemsInBasket(User.Identity.Name, id);
            }

            SingleRestaurantViewModel singleRestaurantViewModel = new SingleRestaurantViewModel()
            {
                Restaurant = restaurant,
                NumberOfCartItems = numberOfCartItems
            };
            return View(singleRestaurantViewModel);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            if (!await restaurantService.ValidateAccessAsync(id, User))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            await restaurantService.DeleteRestaurantAsync(id);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Rate(int stars, string oppinion, long id)
        {
            string username = User.Identity.Name;
            await restaurantService.SaveUserRatingAsync(username, stars, id, oppinion);
            return RedirectToAction(nameof(RestaurantController.Index), "Restaurant", new { id = id });
        }
    }
}