using Microsoft.AspNetCore.Mvc;

namespace Online_Quiz_Platform.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // You can also pass data like username via TempData
            var email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;
            return View();
        }
    }
}
