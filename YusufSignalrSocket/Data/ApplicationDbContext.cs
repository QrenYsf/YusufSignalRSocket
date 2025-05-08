using Microsoft.EntityFrameworkCore;
using YusufSignalrSocket.Models;

namespace YusufSignalrSocket.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
