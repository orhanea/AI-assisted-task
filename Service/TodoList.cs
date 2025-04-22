using System.ComponentModel.DataAnnotations;

namespace TodoListApp.WebApi.Service
{
    public class TodoList
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "It is required to enter a list title.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "It is required to enter a list description.")]
        public string? Description { get; set; }

        public int NumberOfTasks { get; set; }

        public bool IsShared { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
