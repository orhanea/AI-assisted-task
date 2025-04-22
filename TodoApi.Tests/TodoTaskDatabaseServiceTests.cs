using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoListApp.WebApi.Data;
using TodoListApp.WebApi.Service;
using Xunit;

namespace TodoApi.Tests
{
    public class TodoTaskDatabaseServiceTests : IDisposable // Implement IDisposable
    {
        private readonly TodoDbContext _context;
        private readonly TodoTaskDatabaseService _service;
        private bool _disposed; // Track whether Dispose has been called

        public TodoTaskDatabaseServiceTests()
        {
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(databaseName: "TodoDbTest")
                .Options;
            _context = new TodoDbContext(options);
            _service = new TodoTaskDatabaseService(_context);
        }

        [Fact]
        public async Task CreateTodoTaskAsync_AddsTaskToDatabase()
        {
            // Arrange
            var newTask = new TodoTaskPostDto { Title = "Test Task", Description = "Test Description" };

            // Act
            await _service.CreateTodoTaskAsync(newTask);
            var tasks = await _service.GetTodoTasksAsync();

            // Assert
            Assert.Single(tasks);
        }

        [Fact]
        public async Task DeleteTodoTaskAsync_RemovesTaskFromDatabase()
        {
            // Arrange
            var task = new TodoTaskEntity { Title = "Task to Delete", Description = "Description" };
            _context.TodoTasks.Add(task);
            await _context.SaveChangesAsync();

            // Act
            await _service.DeleteTodoTaskAsync(task.Id);
            var tasks = await _service.GetTodoTasksAsync();

            // Assert
            Assert.Empty(tasks);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Suppress finalization
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    _context?.Dispose();
                }

                // Free unmanaged resources (if any)
                _disposed = true;
            }
        }

        ~TodoTaskDatabaseServiceTests()
        {
            Dispose(false); // Finalizer calls Dispose(false)
        }
    }
}
