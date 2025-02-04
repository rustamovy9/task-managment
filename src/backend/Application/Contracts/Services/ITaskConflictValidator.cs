using Application.DTO_s;
using Application.Extensions.ResultPattern;
using Domain.Entities;
using Domain.Enums;

namespace Application.Contracts.Services;

public interface ITaskConflictValidator
{
    Task<BaseResult> CheckForConflictAsync(TaskCreateInfo taskInfo);
    Task<BaseResult> CheckForConflictAsync(int taskId,TaskUpdateInfo taskInfo);

    protected Task<BaseResult> ValidateCommonConflictsAsync(string title, int assignedToUserId, Status status,
        DateTimeOffset deadline, int? taskId);
}