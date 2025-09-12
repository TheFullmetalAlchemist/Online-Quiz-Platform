using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index(Register register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            if (await _context.Registers.AnyAsync(x => x.Email == register.Email))
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
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Login");
        }
    }
}
