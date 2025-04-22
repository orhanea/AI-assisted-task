using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoListApp.WebApi.Data;

namespace TodoApi.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<TodoListEntity> TodoLists { get; set; }

        public DbSet<TodoTaskEntity> TodoTasks { get; set; }

    }
}
