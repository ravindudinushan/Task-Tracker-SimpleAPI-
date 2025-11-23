using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context) => _context = context;

    [HttpPost]
public async Task<ActionResult<TaskItem>> CreateTask(TaskDto dto)
{
    var task = new TaskItem
    {
        Title = dto.Title,
        Description = dto.Description,
        Status = dto.Status,
        DueDate = dto.DueDate,
        UserId = dto.UserId
    };

    _context.Tasks.Add(task);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
}


    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
    {
        return await _context.Tasks.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetTaskById(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        return task == null ? NotFound() : Ok(task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, TaskDto dto)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Status = dto.Status;
        task.DueDate = dto.DueDate;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{taskId}/assign/{userId}")]
    public async Task<IActionResult> AssignTask(int taskId, int userId)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        var user = await _context.Users.FindAsync(userId);
        if (task == null || user == null) return NotFound();

        task.UserId = userId;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}