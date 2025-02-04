using Domain.Common;

namespace Domain.Entities;

public sealed class Comment : BaseEntity
{
    public string Content { get; set; } = default!;

    public int TaskId { get; set; }
    public Tasks Tasks { get; set; } = default!;

    public int UserId { get; set; }
    public User User { get; set; } = default!;
}