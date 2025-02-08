namespace Application.DTO_s;

public interface IBaseCommentInfo
{
    public string Content { get; init; }
    public int UserId { get; init; }
    public int TaskId { get; init; }
}

public readonly record struct CommentReadInfo(
    string Content,
    int UserId,
    int TaskId,  
    int Id):IBaseCommentInfo;

public  record  CommentCreateInfo(
    string Content,
    int UserId,
    int TaskId) : IBaseCommentInfo;


public  record  CommentUpdateInfo(
    string Content,
    int UserId,
    int TaskId) : IBaseCommentInfo;
