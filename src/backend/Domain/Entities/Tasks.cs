using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public sealed class Tasks : BaseEntity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public DateTimeOffset DeadLine { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public ICollection<Comment> Comments { get; set; }
}