using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Online_Quiz_Platform.Data;
using Online_Quiz_Platform.Models.Entities;

namespace Online_Quiz_Platform.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Login login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            // Find user by email
            var user = _context.Registers.FirstOrDefault(r => r.Email == login.Email);

            // Validate user existence and password
            if (user == null || !VerifyPassword(login.Password, user.Password))
            {
                ModelState.AddModelError("", "Invalid login credentials.");
                return View(login);
            }

            // Save user session
            HttpContext.Session.SetString("UserName", user.Name);

            return RedirectToAction("Index", "Home");
        }

        // Temporary method for password check (replace with real hashing)
        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            return enteredPassword == storedPassword;
        }

    }

}

