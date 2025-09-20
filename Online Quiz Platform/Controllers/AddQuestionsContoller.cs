using Microsoft.AspNetCore.Mvc;
using Online_Quiz_Platform.Data;
using Online_Quiz_Platform.Models.Entities;
using System.Security.Claims;

public class AddQuestionsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AddQuestionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult QuizList()
    {
        if (!IsCreator(out var redirectResult))
        {
            return redirectResult;
        }

        var quizzes = _context.Quizzes.ToList();
        ViewBag.Quizzes = quizzes;

        return View();
    }

    [HttpGet]
    public IActionResult Create(int quizId)
    {
        if (!IsCreator(out var redirectResult))
        {
            return redirectResult;
        }

        ViewBag.QuizId = quizId;
        return View();
    }

    [HttpPost]
    [ActionName("Create")]   
    public IActionResult CreatePost(int quizId, string text, List<string> optionTexts, int correctOptionIndex)
    {
        if (!IsCreator(out var redirectResult))
        {
            return redirectResult;
        }

        if (ModelState.IsValid)
        {
            var question = new Question
            {
                Id = Guid.NewGuid(),
                QuizId = quizId,
                Text = text,
                Options = new List<Option>()
            };

            foreach (var optionText in optionTexts)
            {
                var option = new Option
                {
                    Id = Guid.NewGuid(),
                    Text = optionText,
                    QuestionId = question.Id
                };
                question.Options.Add(option);
            }

            _context.Questions.Add(question);
            _context.SaveChanges();

            var correctOption = question.Options[correctOptionIndex];
            question.Correctoption = correctOption.Id;
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Question added successfully!";
            return RedirectToAction("Create", new { quizId });
        }

        ViewBag.QuizId = quizId;
        return View();
    }


    // Helper method to check if user is creator
    private bool IsCreator(out IActionResult redirectResult)
    {
        redirectResult = null!;
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(userEmail))
        {
            TempData["ErrorMessage"] = "You must be logged in.";
            redirectResult = RedirectToAction("Index", "Login");
            return false;
        }

        var user = _context.Registers.FirstOrDefault(u => u.Email == userEmail);
        if (user == null)
        {
            TempData["ErrorMessage"] = "Invalid session. Please log in again.";
            redirectResult = RedirectToAction("Index", "Login");
            return false;
        }

        if (user.CreatorFlag != "Y")
        {
            TempData["ErrorMessage"] = "Only creators can add questions.";
            redirectResult = RedirectToAction("Index", "Home");
            return false;
        }

        return true;
    }
}
