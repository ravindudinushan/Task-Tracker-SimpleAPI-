public class TaskDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = "To Do";
    public DateTime? DueDate { get; set; }

    public int? UserId { get; set; }
}