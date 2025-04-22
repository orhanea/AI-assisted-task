using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoListApp.WebApi.Service;

namespace TodoListApp.WebApi.Data;

public class TodoListDatabaseService : ITodoListDatabaseService
{
    private readonly TodoDbContext _context;
    public TodoListDatabaseService(TodoDbContext context)
    {
        this._context = context;
    }
    public async Task<IEnumerable<TodoList>> GetTodoListsAsync()
    {
        return await this._context.TodoLists
            .Select(todoList => new TodoList
            {
                Id = todoList.Id,
                Title = todoList.Title,
                Description = todoList.Description,
                NumberOfTasks = todoList.NumberOfTasks,
                IsShared = todoList.IsShared,
                CreatedAt = todoList.CreatedAt
            })
            .ToListAsync();
    }
    public async Task<TodoList?> GetTodoListByIdAsync(int id)
    {
        var todoList = await this._context.TodoLists.FindAsync(id);
        return todoList is null
            ? null
            : new TodoList
            {
                Id = todoList.Id,
                Title = todoList.Title,
                Description = todoList.Description,
                NumberOfTasks = todoList.NumberOfTasks,
                IsShared = todoList.IsShared,
                CreatedAt = todoList.CreatedAt
            };
    }
    public async Task CreateTodoListAsync(TodoList todoList)
    {
        var todoListEntity = new TodoListEntity
        {
            Title = todoList.Title,
            Description = todoList.Description,
            NumberOfTasks = todoList.NumberOfTasks,
            IsShared = todoList.IsShared,
            CreatedAt = todoList.CreatedAt
        };
        _ = this._context.TodoLists.Add(todoListEntity);
        _ = await this._context.SaveChangesAsync();
    }
    public async Task UpdateTodoListAsync(TodoList todoList)
    {
        var todoListEntity = await this._context.TodoLists.FindAsync(todoList.Id);
        if (todoListEntity is null)
        {
            throw new InvalidOperationException("Todo list not found.");
        }
        todoListEntity.Title = todoList.Title;
        todoListEntity.Description = todoList.Description;
        _ = await this._context.SaveChangesAsync();
    }
    public async Task DeleteTodoListAsync(int id)
    {
        var todoListEntity = await this._context.TodoLists.FindAsync(id);
        if (todoListEntity is null)
        {
            throw new InvalidOperationException("Todo list not found.");
        }
        _ = this._context.TodoLists.Remove(todoListEntity);
        _ = await this._context.SaveChangesAsync();
    }
}
