using Microsoft.AspNetCore.Mvc;

namespace Online_Quiz_Platform.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // You can also pass data like username via TempData
            ViewBag.UserEmail = TempData["UserEmail"];
            return View();
        }
    }
}
