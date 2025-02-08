using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public sealed class TaskHistory : BaseEntity
{
    public string ChangeDescription { get; set; } = default!;
    public DateTimeOffset ChangedAt  { get; set; }
    
    public int TasksId { get; set; }
    public Tasks Task { get; set; } = default!;
    
    public int UserId { get; set; }
    public User User { get; set; } = default!;

}