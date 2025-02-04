using Domain.Common;
using Domain.Enums;

namespace Application.Filters;

public  record TaskFilter(
    Status? Status,
    Priority? Priority,
    string? Title,
    string? Description,
    DateTimeOffset? Deadline,
    int? AssignedToUserId) : BaseFilter;