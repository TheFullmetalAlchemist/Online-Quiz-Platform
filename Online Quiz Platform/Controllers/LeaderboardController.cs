using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Quiz_Platform.Data;

namespace Online_Quiz_Platform.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaderboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var leaderboard = _context.QuizAttempts
                .GroupBy(a => a.UserName)  // group by user
                .Select(g => new
                {
                    UserName = g.Key,
                    TotalScore = g.Sum(a => a.Score),   // sum of all scores
                    LastPlayed = g.Max(a => a.AttemptDate)
                })
                .OrderByDescending(x => x.TotalScore)
                .Take(5)   // top 5 users
                .ToList();

            return View(leaderboard);
        }
    }
}
