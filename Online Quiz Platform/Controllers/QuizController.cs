using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Quiz_Platform.Data;
using Online_Quiz_Platform.Models.Entities;

namespace Online_Quiz_Platform.Controllers
{
    public class QuizController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuizController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Step 1: Show all questions with options in a single form
        public IActionResult Start()
        {
            var questions = _context.Questions
                .Include(q => q.Options)
                .ToList();
            return View(questions);
        }

        [HttpPost]
        public IActionResult SubmitQuiz(Dictionary<Guid, Guid> answers)
        {
            int totalQuestions = answers.Count;
            int correctAnswers = 0;

            // ✅ Get the quiz from the first question in the answers
            var firstQuestionId = answers.Keys.FirstOrDefault();
            var question = _context.Questions.FirstOrDefault(q => q.Id == firstQuestionId);

            if (question == null)
            {
                return NotFound("Invalid quiz submission. Question not found.");
            }

            var quiz = _context.Quizzes.FirstOrDefault(q => q.Id == question.QuizId);

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

            int scorePercentage = (int)((double)correctAnswers / totalQuestions * 100);

            // ✅ Save the attempt with a valid QuizId
            var attempt = new QuizAttempt
            {
                UserName = HttpContext.Session.GetString("UserName") ?? "Guest",
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


