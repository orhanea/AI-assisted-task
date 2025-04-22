using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoListApp.WebApi.Service;

namespace TodoListApp.WebApi.Data;

public class TodoTaskDatabaseService : ITodoTaskDatabaseService
{
    private readonly TodoDbContext context;
    public TodoTaskDatabaseService(TodoDbContext todoListDbContext)
    {
        this.context = todoListDbContext;
    }

    public async Task CreateTodoTaskAsync(TodoTaskPostDto todoTask)
    {
        var entity = new TodoTaskEntity
        {
            TodoListId = todoTask.TodoListId,
            Title = todoTask.Title,
            Description = todoTask.Description
        };

        _ = this.context.TodoTasks.Add(entity);
        _ = await this.context.SaveChangesAsync();
    }

    public async Task DeleteTodoTaskAsync(int id)
    {
        var entity = await this.context.TodoTasks.FindAsync(id);
        if (entity != null)
        {
            _ = this.context.TodoTasks.Remove(entity);
            _ = await this.context.SaveChangesAsync();
        }
    }

    public async Task<TodoTask?> GetTodoTaskByIdAsync(int id)
    {
        var entity = await this.context.TodoTasks.FindAsync(id);
        if (entity != null)
        {
            return new TodoTask
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                DueDate = entity.DueDate
            };
        }

        return null;
    }

    public async Task<IEnumerable<TodoTask>> GetTodoTasksAsync()
    {
        return await this.context.TodoTasks
            .Select(t => new TodoTask
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                CreatedAt = t.CreatedAt,
                DueDate = t.DueDate
            })
            .ToListAsync();
    }

    public async Task UpdateTodoTaskAsync(TodoTaskPostDto todoTask)
    {
        var entity = await this.context.TodoTasks.FindAsync(todoTask.Id);
        if (entity != null)
        {
            entity.Title = todoTask.Title;
            entity.Description = todoTask.Description;

            _ = await this.context.SaveChangesAsync();
        }
    }
}
