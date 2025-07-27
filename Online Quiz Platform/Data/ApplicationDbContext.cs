using Microsoft.EntityFrameworkCore;
using Online_Quiz_Platform.Models.Entities;

namespace Online_Quiz_Platform.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Login> Logins { get; set; }

    }
}
