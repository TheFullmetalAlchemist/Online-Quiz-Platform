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

        // Step 1: Show all questions with options in a single form
        [HttpGet]
        public IActionResult Start(int quizId)
        {
            var quiz = _context.Quizzes.FirstOrDefault(q => q.Id == quizId);

            if (quiz == null)
            {
                return NotFound("Quiz not found.");
            }

            var questions = _context.Questions
                .Include(q => q.Options)
                .Where(q => q.QuizId == quizId)
                .ToList();

            ViewBag.QuizTitle = quiz.Title;
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

    }

}


