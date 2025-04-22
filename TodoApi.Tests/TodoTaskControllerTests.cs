using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoListApp.WebApi.Controllers;
using TodoListApp.WebApi.Service;
using Xunit;

namespace TodoApi.Tests
{
    public class TodoTaskControllerTests
    {
        private readonly Mock<ITodoTaskDatabaseService> _mockService;
        private readonly TodoTaskController _controller;

        public TodoTaskControllerTests()
        {
            _mockService = new Mock<ITodoTaskDatabaseService>();
            _controller = new TodoTaskController(_mockService.Object);
        }

        [Fact]
        public async Task GetTodoTasks_ReturnsOkResult_WithListOfTasks()
        {
            // Arrange
            var mockTasks = new List<TodoTask>
            {
                new TodoTask { Id = 1, Title = "Task 1", Description = "Description 1" },
                new TodoTask { Id = 2, Title = "Task 2", Description = "Description 2" }
            };
            _mockService.Setup(s => s.GetTodoTasksAsync()).ReturnsAsync(mockTasks);

            // Act
            var result = await _controller.GetTodoTasks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTasks = Assert.IsAssignableFrom<IEnumerable<TodoTask>>(okResult.Value);
            Assert.Equal(2, returnedTasks.Count());
        }

        [Fact]
        public async Task GetTodoTaskById_ReturnsNotFound_WhenTaskDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetTodoTaskByIdAsync(It.IsAny<int>())).ReturnsAsync((TodoTask?)null);

            // Act
            var result = await _controller.GetTodoTaskById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateTodoTask_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var newTask = new TodoTaskPostDto { Id = 1, Title = "New Task", Description = "New Description" };

            // Act
            var result = await _controller.CreateTodoTask(newTask);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetTodoTaskById), createdResult.ActionName);
        }
    }
}
