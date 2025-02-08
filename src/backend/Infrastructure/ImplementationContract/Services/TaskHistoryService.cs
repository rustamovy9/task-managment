using System.Linq.Expressions;
using Application.Contracts.Repositories;
using Application.Contracts.Services;
using Application.DTO_s;
using Application.Extensions.Mappers;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Extensions;

namespace Infrastructure.ImplementationContract.Services;

public class TaskHistoryService (ITaskHistoryRepository repository,ITaskHistoryConflictValidator taskHistoryConflictValidator) : ITaskHistoryService
{
     public async Task<Result<PagedResponse<IEnumerable<TaskHistoryReadInfo>>>> GetAllAsync(TaskHistoryFilter filter)
    {
        return await Task.Run(() =>
        {
                Expression<Func<TaskHistory, bool>> filterExpression = taskHistory =>
                    (string.IsNullOrEmpty(filter.ChangeDescription) || taskHistory.ChangeDescription.ToLower().Contains(filter.ChangeDescription.ToLower())) &&
                    (filter.ChangedAt == null || taskHistory.ChangedAt== filter.ChangedAt) &&
                    (filter.TaskId == null || taskHistory.TasksId == filter.TaskId) &&
                    (filter.UserId == null || taskHistory.UserId == filter.UserId);

            Result<IQueryable<TaskHistory>> request = repository
                .Find(filterExpression);

            if (!request.IsSuccess)
                return Result<PagedResponse<IEnumerable<TaskHistoryReadInfo>>>.Failure(request.Error);

            List<TaskHistoryReadInfo> query = request.Value!.Select(x => x.ToRead()).ToList();

            int count = query.Count;

            IEnumerable<TaskHistoryReadInfo> task =
                query.Page(filter.PageNumber, filter.PageSize);

            PagedResponse<IEnumerable<TaskHistoryReadInfo>> res =
                PagedResponse<IEnumerable<TaskHistoryReadInfo>>.Create(filter.PageNumber, filter.PageSize, count, task);

            return Result<PagedResponse<IEnumerable<TaskHistoryReadInfo>>>.Success(res);
        });
    }

    public async Task<Result<TaskHistoryReadInfo>> GetByIdAsync(int id)
    {
        Result<TaskHistory?> res = await repository.GetByIdAsync(id);
        if (!res.IsSuccess) return Result<TaskHistoryReadInfo>.Failure(res.Error);

        return Result<TaskHistoryReadInfo>.Success(res.Value!.ToRead());
    }
    

    
    

    public async Task<BaseResult> CreateAsync(TaskHistoryCreateInfo createInfo)
    {
        
        BaseResult conflict = await taskHistoryConflictValidator.ValidateTaskHistoryAsync(createInfo);
        if (!conflict.IsSuccess) return conflict;
        
        Result<int> res = await repository.AddAsync( createInfo.ToEntity());

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<BaseResult> UpdateAsync(int id, TaskHistoryUpdateInfo updateInfo)
    {
        Result<TaskHistory?> res = await repository.GetByIdAsync(id);
        
        BaseResult conflict = await taskHistoryConflictValidator.ValidateTaskHistoryAsync(updateInfo);
        if (!conflict.IsSuccess) return conflict;
        
        
        Result<int> result = await repository.UpdateAsync(res.Value!.ToEntity(updateInfo));

        return result.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(result.Error);
    }

    public async Task<BaseResult> DeleteAsync(int id)
    {
        Result<TaskHistory?> res = await repository.GetByIdAsync(id);
        if (!res.IsSuccess) return BaseResult.Failure(Error.NotFound());
        
        Result<int> result = await repository.DeleteAsync(id);
        return result.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(result.Error);
    }
}