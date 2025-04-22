namespace TodoListApp.WebApi.Service;

public interface ITodoTaskDatabaseService
{
    Task<IEnumerable<TodoTask>> GetTodoTasksAsync();

    Task<TodoTask?> GetTodoTaskByIdAsync(int id);

    Task CreateTodoTaskAsync(TodoTaskPostDto todoTask);

    Task UpdateTodoTaskAsync(TodoTaskPostDto todoTask);

    Task DeleteTodoTaskAsync(int id);
}
