namespace TodoListApp.WebApi.Service
{
    public interface ITodoListDatabaseService
    {
        Task<IEnumerable<TodoList>> GetTodoListsAsync();

        Task<TodoList?> GetTodoListByIdAsync(int id);

        Task CreateTodoListAsync(TodoList todoList);

        Task UpdateTodoListAsync(TodoList todoList);

        Task DeleteTodoListAsync(int id);
    }
}
