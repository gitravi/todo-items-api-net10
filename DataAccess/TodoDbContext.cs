using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
namespace TodoApi.DataAccess;

// Represents the database context for the to-do application, managing the TodoItems table.
public class TodoDbContext : DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }
}