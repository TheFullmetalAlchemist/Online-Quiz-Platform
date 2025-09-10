using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Quiz_Platform.Data;
using System.Security.Claims;

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
            // Get claim value safely
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim, out var userId))
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



