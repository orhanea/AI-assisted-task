namespace TodoListApp.WebApi.Data;

public class TodoTaskEntity
{
    public int Id { get; set; }

    public int TodoListId { get; set; }

    public TodoListEntity? TodoList { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime DueDate { get; set; }
}
