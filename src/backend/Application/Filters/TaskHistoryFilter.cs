using Domain.Common;

namespace Application.Filters;

public record TaskHistoryFilter(
    string? ChangeDescription,
    DateTimeOffset? ChangedAt,
    int? TaskId,
    int? UserId) : BaseFilter;