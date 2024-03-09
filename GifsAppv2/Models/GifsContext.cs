using Microsoft.EntityFrameworkCore;

namespace GifsAppv2.Models
{
    public class GifsContext : DbContext
    {
        public GifsContext(DbContextOptions<GifsContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; } = null!;
    }
}
