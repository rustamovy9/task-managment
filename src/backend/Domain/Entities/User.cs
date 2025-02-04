using Domain.Common;
using Domain.Constants;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; } = default!;
    public string FirstName { get; set; } = default!; 
    public string LastName { get; set; } = default!;
    public DateTimeOffset DateOfBirth { get; set; }
    public string Email { get; set; } = default!; 
    public string? PhoneNumber { get; set; } 
    public string? Address { get; set; } 
    public string AvatarPath { get; set; } = FileData.Default;
    public string PasswordHash { get; set; } = default!;
    public ICollection<UserRole> UserRoles { get; set; } = [];
    public ICollection<Tasks> Tasks { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<TaskHistory> TaskHistories { get; set; } = [];
}
