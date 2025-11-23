using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context) => _context = context;

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(UserDto dto)
    {
        var user = new User { Name = dto.Name, Email = dto.Email };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    [HttpGet("{id}/tasks")]
public async Task<ActionResult<IEnumerable<TaskItem>>> GetUserTasks(int id)
{
    try
    {
        var user = await _context.Users
            .Include(u => u.Tasks)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return NotFound();

        return Ok(user.Tasks);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal error: {ex.Message}");
    }
}
}