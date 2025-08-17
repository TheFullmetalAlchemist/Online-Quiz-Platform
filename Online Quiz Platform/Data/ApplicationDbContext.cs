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

        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
    }
}
