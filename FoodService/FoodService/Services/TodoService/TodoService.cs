using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodService.Models;
using FoodService.Models.RequestModels.TodoRequestModels;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Services.TodoService
{
    public class TodoService : ITodoService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper iMapper;

        public TodoService(ApplicationDbContext applicationDbContext, IMapper iMapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.iMapper = iMapper;
        }
        public async Task<Todo> AddTodoAsync(TodoRequest todoRequest)
        {
            var todo = iMapper.Map<TodoRequest, Todo>(todoRequest);
            await applicationDbContext.Todos.AddAsync(todo);
            await applicationDbContext.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> DeleteTodoAsync(long todoId)
        {
            var todo = await FindTodoByIdAsync(todoId);
            if (todo != null)
            {
                applicationDbContext.Todos.Remove(todo);
                applicationDbContext.SaveChanges();
            }
            return todo;
        }

        public async Task<Todo> EditTodoAsync(TodoRequest todoRequest, long todoId)
        {
            var todo = await FindTodoByIdAsync(todoId);
            todo = iMapper.Map<TodoRequest, Todo>(todoRequest, todo);
            await applicationDbContext.SaveChangesAsync();
            return todo;
        }

        public async Task<List<Todo>> FindAllAsync()
        {
            var todoList = await applicationDbContext.Todos.ToListAsync();
            return todoList;
        }

        public async Task<Todo> FindTodoByIdAsync(long todoId)
        {
            var todo = await applicationDbContext.Todos.FirstOrDefaultAsync(t => t.TodoId == todoId);
            return todo;
        }

        public async Task<TodoRequest> CreateTodoRequestAsync(long todoId)
        {
            var todo = await FindTodoByIdAsync(todoId);
            if (todo != null)
            {
                var todoRequest = iMapper.Map<Todo, TodoRequest>(todo);
                return todoRequest;
            }
            return null;
        }
    }
}
