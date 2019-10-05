using FoodService.Models;
using FoodService.Models.RequestModels.TodoRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.TodoService
{
    interface ITodoService
    {
        Task<List<Todo>> FindAllAsync();
        Task<Todo> FindTodoByIdAsync(long postId);
        Task<Todo> AddTodoAsync(TodoRequest todoRequest);
        Task<Todo> EditTodoAsync(TodoRequest todoRequest);
        Task<Todo> DeleteTodoAsync(long todoId);
    }
}
