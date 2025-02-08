using Application.Contracts.Services;
using Application.DTO_s;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ImplementationContract.Services;

public sealed class TaskHistoryConflictValidator(DataContext context) : ITaskHistoryConflictValidator
{
    public async Task<BaseResult> ValidateTaskHistoryAsync(TaskHistoryCreateInfo request)
    {
        if (await context.TaskHistories.AnyAsync(h => 
                h.TasksId == request.TaskId && 
                h.ChangeDescription == request.ChangeDescription))
        {
            return BaseResult.Failure(Error.Conflict("This task change is already recorded."));
        }

        if (!await context.Tasks.AnyAsync(t => t.Id == request.TaskId))
        {
            return BaseResult.Failure(Error.Conflict("Task does not exist."));
        }

        if (!await context.Users.AnyAsync(u => u.Id == request.UserId))
        {
            return BaseResult.Failure(Error.Conflict("User does not exist."));
        }

        return BaseResult.Success();
    }
    public async Task<BaseResult> ValidateTaskHistoryAsync(TaskHistoryUpdateInfo request)
    {
        if (await context.TaskHistories.AnyAsync(h => 
                h.TasksId == request.TaskId && 
                h.ChangeDescription == request.ChangeDescription))
        {
            return BaseResult.Failure(Error.Conflict("This task change is already recorded."));
        }

        if (!await context.Tasks.AnyAsync(t => t.Id == request.TaskId))
        {
            return BaseResult.Failure(Error.Conflict("Task does not exist."));
        }

        if (!await context.Users.AnyAsync(u => u.Id == request.UserId))
        {
            return BaseResult.Failure(Error.Conflict("User does not exist."));
        }

        return BaseResult.Success();
    }

}
