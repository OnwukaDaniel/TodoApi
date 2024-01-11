using Microsoft.EntityFrameworkCore;

namespace Management.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options): base(options) {
        }

        public DbSet<UserProfile> UserProfile { get; set; } = null!;
    }
}
