using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Quiz_Platform.Data;
using Online_Quiz_Platform.Models.Entities;
using System.Linq;

namespace Online_Quiz_Platform.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaderboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string sortColumn = "Score", string sortOrder = "desc")
        {
            IQueryable<QuizAttempt> leaderboard = _context.QuizAttempts
                .Include(a => a.Quiz); 

            // Sorting logic
            switch (sortColumn.ToLower())
            {
                case "username":
                    leaderboard = (sortOrder == "asc")
                        ? leaderboard.OrderBy(a => a.UserName)
                        : leaderboard.OrderByDescending(a => a.UserName);
                    break;

                case "score":
                    leaderboard = (sortOrder == "asc")
                        ? leaderboard.OrderBy(a => a.Score)
                        : leaderboard.OrderByDescending(a => a.Score);
                    break;

                case "attemptdate":
                    leaderboard = (sortOrder == "asc")
                        ? leaderboard.OrderBy(a => a.AttemptDate)
                        : leaderboard.OrderByDescending(a => a.AttemptDate);
                    break;

                case "quiz":
                    leaderboard = (sortOrder == "asc")
                        ? leaderboard.OrderBy(a => a.Quiz != null ? a.Quiz.Title : "")
                        : leaderboard.OrderByDescending(a => a.Quiz != null ? a.Quiz.Title : "");
                    break;

                default:
                    leaderboard = leaderboard.OrderByDescending(a => a.Score);
                    break;
            }

            ViewBag.CurrentSortColumn = sortColumn;
            ViewBag.CurrentSortOrder = sortOrder;

            return View(leaderboard.ToList());
        }
    }
}
