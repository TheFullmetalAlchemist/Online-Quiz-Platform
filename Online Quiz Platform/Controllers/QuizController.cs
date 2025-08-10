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

        // Step 2: Handle all answers at once
        [HttpPost]
        public IActionResult SubmitQuiz(Dictionary<Guid, Guid> answers)
        {
            int totalQuestions = answers.Count;
            int correctAnswers = 0;

            foreach (var answer in answers)
            {
                var questionId = answer.Key;
                var selectedOptionId = answer.Value;

                var question = _context.Questions.FirstOrDefault(q => q.Id == questionId);

                if (question != null && question.Correctoption == selectedOptionId)
                {
                    correctAnswers++;
                }
            }

            ViewBag.TotalQuestions = totalQuestions;
            ViewBag.CorrectAnswers = correctAnswers;
            ViewBag.ScorePercentage = (int)((double)correctAnswers / totalQuestions * 100);

            return View("FinalScore");
        }
    }
}
