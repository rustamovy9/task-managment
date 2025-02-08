using Domain.Common;

namespace Domain.Entities;

public sealed class Comment : BaseEntity
{
    public string Content { get; set; } = default!;

    public int TasksId { get; set; }
    public Tasks Task { get; set; } = default!;

    public int UserId { get; set; }
    public User User { get; set; } = default!;
}