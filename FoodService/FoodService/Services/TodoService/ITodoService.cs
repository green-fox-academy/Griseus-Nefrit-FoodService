using FoodService.Models;
using FoodService.Models.RequestModels.TodoRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.TodoService
{
    public interface ITodoService
    {
        Task<List<Todo>> FindAllAsync();
        Task<Todo> FindTodoByIdAsync(long todoId);
        Task<Todo> AddTodoAsync(TodoRequest todoRequest);
        Task<Todo> EditTodoAsync(TodoRequest todoRequest, long todoId);
        Task<Todo> DeleteTodoAsync(long todoId);
        Task<TodoRequest> CreateTodoRequestAsync(long todoId);
    }
}
