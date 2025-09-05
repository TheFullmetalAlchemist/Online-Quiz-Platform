using Microsoft.AspNetCore.Mvc;
using Online_Quiz_Platform.Data;

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
        public IActionResult Index(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Please enter email and password.";
                return View();
            }

            var user = _context.Registers
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid login credentials.";
                return View();
            }

            // Save session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.Name);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
