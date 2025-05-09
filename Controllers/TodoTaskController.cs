using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApi.Service;

namespace TodoListApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoTaskController : ControllerBase
{
    private readonly ITodoTaskDatabaseService todoTaskDatabaseService;

    public TodoTaskController(ITodoTaskDatabaseService todoTaskDbService)
    {
        this.todoTaskDatabaseService = todoTaskDbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodoTasks()
    {
        var todoTasks = await this.todoTaskDatabaseService.GetTodoTasksAsync();
        return this.Ok(todoTasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoTaskById(int id)
    {
        var todoTask = await this.todoTaskDatabaseService.GetTodoTaskByIdAsync(id);
        if (todoTask == null)
        {
            return this.NotFound();
        }

        return this.Ok(todoTask);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodoTask(TodoTaskPostDto todoTask)
    {
        await this.todoTaskDatabaseService.CreateTodoTaskAsync(todoTask);
        return this.CreatedAtAction(nameof(GetTodoTaskById), new { id = todoTask.Id }, todoTask);
    }

    [HttpPost("{todoTask.Id}")]
    public async Task<IActionResult> UpdateTodoTask([FromBody] TodoTaskPostDto todoList)
    {
        await this.todoTaskDatabaseService.UpdateTodoTaskAsync(todoList);
        return this.Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoTask(int id)
    {
        await this.todoTaskDatabaseService.DeleteTodoTaskAsync(id);
        return this.NoContent();
    }
}
