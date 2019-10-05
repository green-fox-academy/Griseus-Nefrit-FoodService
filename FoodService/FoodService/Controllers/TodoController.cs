using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.TodoRequestModels;
using FoodService.Services.TodoService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodService.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TodoController : Controller
    {
        private readonly ITodoService todoService;

        public TodoController(ITodoService todoService)
        {
            this.todoService = todoService;
        }

        public async Task<IActionResult> Index()
        {
            List<Todo> todos = await todoService.FindAllAsync();
            return View(todos);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(TodoRequest todoRequest)
        {
            if (ModelState.IsValid)
            {
                await todoService.AddTodoAsync(todoRequest);
                return RedirectToAction(nameof(TodoController.Index), "Todo");
            }
            return View(todoRequest);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long Id)
        {
            var todoRequest = await todoService.CreateTodoRequestAsync(Id);
            if (todoRequest == null)
            {
                return RedirectToAction(nameof(TodoController.Index), "Todo");
            }
            return View(todoRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TodoRequest todoRequest, long Id)
        {
            if (ModelState.IsValid)
            {
                var todo = await todoService.FindTodoByIdAsync(Id);
                if (todo != null)
                {
                    await todoService.EditTodoAsync(todoRequest, Id);
                }
                return RedirectToAction(nameof(TodoController.Index), "Todo");
            }
            return View(todoRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long Id)
        {
            var todo = await todoService.FindTodoByIdAsync(Id);
            if (todo != null)
            {

            }
            await todoService.DeleteTodoAsync(Id);
            return RedirectToAction(nameof(TodoController.Index), "Todo");
        }
    }
}