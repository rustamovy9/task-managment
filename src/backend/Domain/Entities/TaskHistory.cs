using Domain.Common;

namespace Domain.Entities;

public sealed class TaskHistory : BaseEntity
{
    public string ChangeDescription { get; set; } = default!;
    public DateTimeOffset ChangedAt  { get; set; }
    
    public int TaskId { get; set; }
    public Tasks Tasks { get; set; } = default!;
    
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public ICollection<TaskHistory> TaskHistories { get; set; } = [];
}