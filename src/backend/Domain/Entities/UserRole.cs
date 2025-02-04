﻿using Domain.Common;

namespace Domain.Entities;

public sealed class UserRole : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    
    public int RoleId { get; set; }
    public Role Role { get; set; } = default!;
}