using Microsoft.AspNetCore.Mvc;
using Online_Quiz_Platform.Data;
using System.Security.Claims;

public class CreatorController : Controller
{
    private readonly ApplicationDbContext _context;

    public CreatorController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult EnableCreator()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var user = _context.Registers.FirstOrDefault(u => u.Id.ToString() == userId);

        if (user == null)
            return NotFound();

        user.CreatorFlag = "Y";
        _context.SaveChanges();

        // ✅ Store message in TempData
        TempData["Message"] = "You are now a Creator!";

        return RedirectToAction("Index", "Account");
    }

    [HttpPost]
    public IActionResult DisableCreator()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var user = _context.Registers.FirstOrDefault(u => u.Id.ToString() == userId);

        if (user == null)
            return NotFound();

        user.CreatorFlag = "N";
        _context.SaveChanges();

        // ✅ Store message in TempData
        TempData["Message"] = "You are no longer a Creator!";

        return RedirectToAction("Index", "Account");
    }

}




