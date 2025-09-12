using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Online_Quiz_Platform.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            ViewBag.UserName = name;
            return View();
        }
    }

}
