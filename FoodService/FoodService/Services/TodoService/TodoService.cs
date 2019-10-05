using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;
using FoodService.Models.RequestModels.TodoRequestModels;

namespace FoodService.Services.TodoService
{
    public class TodoService : ITodoService
    {
        public Task<Todo> AddTodoAsync(TodoRequest todoRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Todo> DeleteTodoAsync(long todoId)
        {
            throw new NotImplementedException();
        }

        public Task<Todo> EditTodoAsync(TodoRequest todoRequest)
        {
            throw new NotImplementedException();
        }

        public Task<List<Todo>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Todo> FindTodoByIdAsync(long postId)
        {
            throw new NotImplementedException();
        }
    }
}
