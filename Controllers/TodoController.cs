using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoListApp.WebApi.Service;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListDatabaseService _todoListDatabaseService;

        public TodoListController(ITodoListDatabaseService todoListDatabaseService)
        {
            this._todoListDatabaseService = todoListDatabaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodoLists()
        {
            var todoLists = await this._todoListDatabaseService.GetTodoListsAsync();
            return this.Ok(todoLists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoListById(int id)
        {
            var todoList = await this._todoListDatabaseService.GetTodoListByIdAsync(id);
            if (todoList == null)
            {
                return this.NotFound();
            }

            return this.Ok(todoList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoList(TodoList todoList)
        {
            await this._todoListDatabaseService.CreateTodoListAsync(todoList);
            return this.CreatedAtAction(nameof(GetTodoListById), new { id = todoList.Id }, todoList);
        }

        [HttpPost("{todoList.Id}")]
        public async Task<IActionResult> UpdateTodoList([FromBody] TodoList todoList)
        {
            await this._todoListDatabaseService.UpdateTodoListAsync(todoList);
            return this.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(int id)
        {
            await this._todoListDatabaseService.DeleteTodoListAsync(id);
            return this.NoContent();
        }
    }
}

