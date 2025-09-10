using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Online_Quiz_Platform.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // You can also pass data like username via TempData
            var name = User.FindFirstValue(ClaimTypes.Name);
            return View();
        }
    }

}
