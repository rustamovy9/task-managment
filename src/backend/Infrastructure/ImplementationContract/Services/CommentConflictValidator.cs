using Application.Contracts.Services;
using Domain.Enums;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ImplementationContract.Services;

public sealed class CommentConflictValidator(DataContext dbContext) : ICommentConflictValidator
{
    public async Task<bool> IsCommentingTooFrequentlyAsync(int userId)
    {
        DateTimeOffset oneMinuteAgo = DateTime.UtcNow.AddMinutes(-1);
        return await dbContext.Comments
            .CountAsync(c => c.UserId == userId && c.CreatedAt >= oneMinuteAgo) >= 5;
    }

    public async Task<bool> IsTaskActiveAsync(int taskId)
    {
        Status task = await dbContext.Tasks
            .Where(t => t.Id == taskId)
            .Select(t => t.Status)
            .FirstOrDefaultAsync();

        return task == Status.InProgress;
    }
}