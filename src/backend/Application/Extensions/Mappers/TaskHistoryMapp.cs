using Application.DTO_s;
using Domain.Entities;
using Domain.Enums;

namespace Application.Extensions.Mappers;

public static class TaskHistoryMapper
{
    public static TaskHistoryReadInfo ToRead(this TaskHistory taskHistory)
    {
        return new TaskHistoryReadInfo(
            taskHistory.ChangeDescription,
            taskHistory.ChangedAt,
            taskHistory.TaskId,
            taskHistory.UserId,
            taskHistory.Id
        );
    }


    public static TaskHistory ToEntity(this TaskHistoryCreateInfo createInfo)
    {
        return new TaskHistory
        {
            ChangeDescription = createInfo.ChangeDescription,
            ChangedAt = DateTimeOffset.UtcNow,
            TaskId = createInfo.TaskId,
            UserId = createInfo.UserId
        };
    }

    public static TaskHistory ToEntity(this TaskHistory entity, TaskHistoryUpdateInfo updateInfo)
    {
        entity.ChangeDescription = updateInfo.ChangeDescription;
        entity.ChangedAt = updateInfo.ChangedAt;
        entity.TaskId = updateInfo.TaskId;
        entity.UserId = updateInfo.UserId;
        entity.Version++;
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        return entity;
    }
}