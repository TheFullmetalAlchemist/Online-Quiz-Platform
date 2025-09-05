using Microsoft.AspNetCore.Mvc;
using Online_Quiz_Platform.Data;

namespace Online_Quiz_Platform.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var account = _context.Registers.FirstOrDefault(u => u.Id == userId);

            if (account == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View(account);
        }
    }
}
