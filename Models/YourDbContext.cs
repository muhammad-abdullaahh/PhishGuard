using Microsoft.EntityFrameworkCore;

namespace PhishGuard.Models
{
    public class YourDbContext : DbContext
    {
        public YourDbContext(DbContextOptions<YourDbContext> options)
            : base(options)
        {
        }

        public DbSet<Link> Links { get; set; }
    }
}
