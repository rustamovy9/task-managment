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

public class TaskService (ITaskRepository repository,ITaskConflictValidator taskConflictValidator) : ITaskService
{
     public async Task<Result<PagedResponse<IEnumerable<TaskReadInfo>>>> GetAllAsync(TaskFilter filter)
    {
        return await Task.Run(() =>
        {
                Expression<Func<Tasks, bool>> filterExpression = task =>
                    (filter.Status == null || task.Status == filter.Status) &&
                    (filter.Priority == null || task.Priority == filter.Priority) &&
                    (string.IsNullOrEmpty(filter.Title) || task.Title.ToLower().Contains(filter.Title.ToLower())) &&
                    (string.IsNullOrEmpty(filter.Description) || task.Description.ToLower().Contains(filter.Description.ToLower())) &&
                    (filter.Deadline == null || task.DeadLine == filter.Deadline) &&
                    (filter.AssignedToUserId == null || task.UserId == filter.AssignedToUserId);

            Result<IQueryable<Tasks>> request = repository
                .Find(filterExpression);

            if (!request.IsSuccess)
                return Result<PagedResponse<IEnumerable<TaskReadInfo>>>.Failure(request.Error);

            List<TaskReadInfo> query = request.Value!.Select(x => x.ToRead()).ToList();

            int count = query.Count;

            IEnumerable<TaskReadInfo> task =
                query.Page(filter.PageNumber, filter.PageSize);

            PagedResponse<IEnumerable<TaskReadInfo>> res =
                PagedResponse<IEnumerable<TaskReadInfo>>.Create(filter.PageNumber, filter.PageSize, count, task);

            return Result<PagedResponse<IEnumerable<TaskReadInfo>>>.Success(res);
        });
    }

    public async Task<Result<TaskReadInfo>> GetByIdAsync(int id)
    {
        Result<Tasks?> res = await repository.GetByIdAsync(id);
        if (!res.IsSuccess) return Result<TaskReadInfo>.Failure(res.Error);

        return Result<TaskReadInfo>.Success(res.Value!.ToRead());
    }
    

    
    

    public async Task<BaseResult> CreateAsync(TaskCreateInfo createInfo)
    {

        BaseResult conflict = await taskConflictValidator.CheckForConflictAsync(createInfo);
        if (!conflict.IsSuccess) return conflict;
        
        Result<int> res = await repository.AddAsync( createInfo.ToEntity());

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<BaseResult> UpdateAsync(int id, TaskUpdateInfo updateInfo)
    {
        Result<Tasks?> res = await repository.GetByIdAsync(id);
        
        BaseResult conflict = await taskConflictValidator.CheckForConflictAsync(id,updateInfo);
        if (!conflict.IsSuccess) return conflict;
        
        
        Result<int> result = await repository.UpdateAsync(res.Value!.ToEntity(updateInfo));

        return result.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(result.Error);
    }

    public async Task<BaseResult> DeleteAsync(int id)
    {
        Result<Tasks?> res = await repository.GetByIdAsync(id);
        if (!res.IsSuccess) return BaseResult.Failure(Error.NotFound());
        
        Result<int> result = await repository.DeleteAsync(id);
        return result.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(result.Error);
    }
}