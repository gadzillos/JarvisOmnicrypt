using Microsoft.EntityFrameworkCore;

namespace JarvisOmnicrypt.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<File> Files { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
