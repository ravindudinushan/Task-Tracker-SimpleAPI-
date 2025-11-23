using System.ComponentModel.DataAnnotations;

public class TaskItem
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "To Do";

    public DateTime? DueDate { get; set; }

    public int? UserId { get; set; }

    public User? User { get; set; }
}
