using Microsoft.AspNetCore.Mvc;
using Online_Quiz_Platform.Data;

[ApiController]
[Route("api/[controller]")]
public class CreatorController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CreatorController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPut("enable/{id}")]
    public IActionResult EnableCreator(int id)
    {
        var user = _context.Registers.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();

        user.CreatorFlag = "Y";
        _context.SaveChanges();

        return Ok(new { message = "User is now a Creator", user });
    }

    [HttpPut("disable/{id}")]
    public IActionResult DisableCreator(int id)
    {
        var user = _context.Registers.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();

        user.CreatorFlag = "N";
        _context.SaveChanges();

        return Ok(new { message = "User is no longer a Creator", user });
    }
}
