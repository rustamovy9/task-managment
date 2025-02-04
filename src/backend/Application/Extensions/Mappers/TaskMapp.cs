using Application.DTO_s;
using Domain.Entities;
using Domain.Enums;

namespace Application.Extensions.Mappers;

public static class TaskMapper
{
    public static TaskReadInfo ToRead(this Tasks task)
    {
        return new TaskReadInfo(
            task.Title,
            task.Description,
            task.Status,
            task.Priority,
            task.DeadLine,
            task.UserId,
            task.Id
        );
    }


    public static Tasks ToEntity(this TaskCreateInfo createInfo)
    {
        return new Tasks
        {
            Title = createInfo.Title,
            Description = createInfo.Description,
            Status = createInfo.Status,
            Priority = createInfo.Priority,
            DeadLine = createInfo.DeadLine,
            UserId = createInfo.AssignedToUserId,
        };
    }

    public static Tasks ToEntity(this Tasks entity, TaskUpdateInfo updateInfo)
    {
        entity.Title = updateInfo.Title;
        entity.Description = updateInfo.Description;
        entity.Status = updateInfo.Status;
        entity.Priority = updateInfo.Priority;
        entity.DeadLine = updateInfo.DeadLine;
        entity.UserId = updateInfo.AssignedToUserId;
        entity.Version++;
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        return entity;
    }
}