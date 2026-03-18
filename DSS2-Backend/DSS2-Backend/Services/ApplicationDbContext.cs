using Microsoft.EntityFrameworkCore;

namespace DSS2_Backend.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
