using Domain.Common;

namespace Application.Filters;

public  record CommentFilter(
    string? Content,
    int? TaskId,
    int? UserId) : BaseFilter;