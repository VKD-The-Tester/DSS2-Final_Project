using DSS2_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace DSS2_Backend.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<TodoItem> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Roles).HasConversion<string>();

            modelBuilder.Entity<TodoItem>().Property(t => t.Priority).HasConversion<string>();
        }
    }
}
