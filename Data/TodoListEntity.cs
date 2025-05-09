namespace TodoListApp.WebApi.Data
{
    public class TodoListEntity
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int NumberOfTasks { get; set; }

        public bool IsShared { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
