using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Quiz_Platform.Data;
using Online_Quiz_Platform.Models.Entities;
using System.Security.Claims;

namespace Online_Quiz_Platform.Controllers
{
    public class QuizController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuizController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SelectQuiz()
        {
            var quizzes = _context.Quizzes.ToList();
            ViewBag.Quizzes = quizzes;
            return View();
        }

        [HttpGet]
        public IActionResult Start(int quizId)
        {
            var questions = _context.Questions
                .Where(q => q.QuizId == quizId)  
                .Include(q => q.Options)
                .ToList();

            if (!questions.Any())
            {
                return Content("No questions found for this quiz."); 
            }

            ViewBag.QuizId = quizId;
            return View(questions);
        }


        [HttpPost]
        public IActionResult SubmitQuiz(int quizId, Dictionary<Guid, Guid> answers)
        {
            int totalQuestions = answers.Count;
            int correctAnswers = 0;

            var quiz = _context.Quizzes.FirstOrDefault(q => q.Id == quizId);
            if (quiz == null)
            {
                return NotFound("Quiz not found.");
            }

            foreach (var answer in answers)
            {
                var q = _context.Questions.FirstOrDefault(x => x.Id == answer.Key);

                if (q != null && q.Correctoption == answer.Value)
                {
                    correctAnswers++;
                }
            }

            int scorePercentage = totalQuestions > 0
                ? (int)((double)correctAnswers / totalQuestions * 100)
                : 0;

            var attempt = new QuizAttempt
            {
                UserName = User.FindFirstValue(ClaimTypes.Name) ?? "Guest",
                Score = scorePercentage,
                AttemptDate = DateTime.Now,
                QuizId = quiz.Id
            };

            _context.QuizAttempts.Add(attempt);
            _context.SaveChanges();

            ViewBag.TotalQuestions = totalQuestions;
            ViewBag.CorrectAnswers = correctAnswers;
            ViewBag.ScorePercentage = scorePercentage;

            return View("FinalScore");
        }
        [HttpGet]
        public IActionResult Create(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                TempData["ErrorMessage"] = "Quiz title is required.";
                return RedirectToAction("SelectQuiz", "Quiz");
            }

            var quiz = new Quiz { Title = title };
            _context.Quizzes.Add(quiz);
            _context.SaveChanges();

            // Redirect directly to Add Questions for this new quiz
            return RedirectToAction("Create", "AddQuestions", new { quizId = quiz.Id });
        }


    }

}


