namespace Application.Contracts.Services;

public interface ICommentConflictValidator
{
    Task<bool> IsCommentingTooFrequentlyAsync(int userId);
    Task<bool>  IsTaskActiveAsync(int taskId);
}