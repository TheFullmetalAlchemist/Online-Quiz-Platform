using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            // Check if the email already exists
            if (register.Password != register.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match");
                return View(register);
            }

            _context.Registers.Add(register);
            _context.SaveChanges();

            return RedirectToAction("Index","Login");
        }
    }
}
