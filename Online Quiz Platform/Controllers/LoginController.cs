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
            // Try to find a matching user
            var user = _context.Registers
                        .FirstOrDefault(r => r.Email == login.Email && r.Password == login.Password);

            if (user == null)
            {
                ModelState.AddModelError("Password", "Invalid login credentials.");
                return View(login);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
