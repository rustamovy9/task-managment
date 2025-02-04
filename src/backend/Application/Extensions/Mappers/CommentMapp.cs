using Application.DTO_s;
using Domain.Entities;
using Domain.Enums;

namespace Application.Extensions.Mappers;

public static class CommentMapper
{
    public static CommentReadInfo ToRead(this Comment comment)
    {
        return new CommentReadInfo(
            comment.Content,
            comment.UserId,
            comment.TaskId,
            comment.Id
        );
    }


    public static Comment ToEntity(this CommentCreateInfo createInfo)
    {
        return new Comment
        {
            Content = createInfo.Content,
            UserId = createInfo.UserId,
            TaskId = createInfo.TaskId
        };
    }

    public static Comment ToEntity(this Comment entity, CommentUpdateInfo updateInfo)
    {
        entity.Content = updateInfo.Content;
        entity.UserId = updateInfo.UserId;
        entity.TaskId = updateInfo.TaskId;
        entity.Version++;
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        return entity;
    }
}