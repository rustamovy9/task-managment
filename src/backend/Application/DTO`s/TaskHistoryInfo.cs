namespace Application.DTO_s;

public interface IBaseTaskHistoryInfo
{
    public string ChangeDescription { get; init; }
    public DateTimeOffset ChangedAt { get; init; } 
    public int TaskId { get; init; }
    public int UserId { get; init; }
}

public readonly record struct TaskHistoryReadInfo(
    string ChangeDescription,
    DateTimeOffset ChangedAt,
    int TaskId,
    int UserId,
    int Id):IBaseTaskHistoryInfo;

public readonly record struct TaskHistoryCreateInfo(
    string ChangeDescription,
    DateTimeOffset ChangedAt,
    int TaskId,
    int UserId) : IBaseTaskHistoryInfo;

public readonly record struct TaskHistoryUpdateInfo(
    string ChangeDescription,
    DateTimeOffset ChangedAt,
    int TaskId,
    int UserId) : IBaseTaskHistoryInfo;
