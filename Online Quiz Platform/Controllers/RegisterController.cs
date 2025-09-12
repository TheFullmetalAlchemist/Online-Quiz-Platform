using Microsoft.AspNetCore.Mvc;
using Online_Quiz_Platform.Data;
using Online_Quiz_Platform.Models.Entities;

namespace Online_Quiz_Platform.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Register register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            if (_context.Registers.Any(x => x.Email == register.Email))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(register);
            }

            if (register.Password != register.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match");
                return View(register);
            }

            register.CreatorFlag = "N";
            _context.Registers.Add(register);
            _context.SaveChanges();

            return RedirectToAction("Index", "Login");
        }
    }
}
