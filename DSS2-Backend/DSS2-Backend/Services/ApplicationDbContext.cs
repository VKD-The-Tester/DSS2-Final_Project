using DSS2_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace DSS2_Backend.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
