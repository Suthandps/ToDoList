using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.DBContext
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Todo> Todo { get; set; }

    }
}
