using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Application.Contracts.Repositories;
using Application.Contracts.Services;
using Application.DTO_s;
using Application.Extensions.Mappers;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;
using Domain.Common;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Extensions;

namespace Infrastructure.ImplementationContract.Services;

public class CommentService (ICommentRepository repository,ICommentConflictValidator commentConflictValidator) : ICommentService
{
     public async Task<Result<PagedResponse<IEnumerable<CommentReadInfo>>>> GetAllAsync(CommentFilter filter)
    {
        return await Task.Run(() =>
        {
                Expression<Func<Comment, bool>> filterExpression = car =>
                    (string.IsNullOrEmpty(filter.Content) || car.Content.ToLower().Contains(filter.Content.ToLower())) &&
                    (filter.UserId == null || car.UserId == filter.UserId) &&
                    (filter.TaskId == null || car.TaskId == filter.TaskId);

            Result<IQueryable<Comment>> request = repository
                .Find(filterExpression);

            if (!request.IsSuccess)
                return Result<PagedResponse<IEnumerable<CommentReadInfo>>>.Failure(request.Error);

            List<CommentReadInfo> query = request.Value!.Select(x => x.ToRead()).ToList();

            int count = query.Count;

            IEnumerable<CommentReadInfo> car =
                query.Page(filter.PageNumber, filter.PageSize);

            PagedResponse<IEnumerable<CommentReadInfo>> res =
                PagedResponse<IEnumerable<CommentReadInfo>>.Create(filter.PageNumber, filter.PageSize, count, car);

            return Result<PagedResponse<IEnumerable<CommentReadInfo>>>.Success(res);
        });
    }

    public async Task<Result<CommentReadInfo>> GetByIdAsync(int id)
    {
        Result<Comment?> res = await repository.GetByIdAsync(id);
        if (!res.IsSuccess) return Result<CommentReadInfo>.Failure(res.Error);

        return Result<CommentReadInfo>.Success(res.Value!.ToRead());
    }
    

    
    

    public async Task<BaseResult> CreateAsync(CommentCreateInfo createInfo)
    {
        
        if (await commentConflictValidator.IsCommentingTooFrequentlyAsync(createInfo.UserId))
            return BaseResult.Failure(Error.Conflict("You are commenting too frequently. Please wait a moment."));

        if (!await commentConflictValidator.IsTaskActiveAsync(createInfo.TaskId))
            BaseResult.Failure(Error.Conflict("Cannot comment on completed or cancelled tasks."));
        
        Result<int> res = await repository.AddAsync( createInfo.ToEntity());

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<BaseResult> UpdateAsync(int id, CommentUpdateInfo updateInfo)
    {
        Result<Comment?> res = await repository.GetByIdAsync(id);

        if (!res.IsSuccess) return BaseResult.Failure(Error.NotFound());
        
        
        if (await commentConflictValidator.IsCommentingTooFrequentlyAsync(updateInfo.UserId))
            return BaseResult.Failure(Error.Conflict("You are commenting too frequently. Please wait a moment."));

        if (!await commentConflictValidator.IsTaskActiveAsync(updateInfo.TaskId))
            BaseResult.Failure(Error.Conflict("Cannot comment on completed or cancelled tasks."));
        
        
        Result<int> result = await repository.UpdateAsync(res.Value!.ToEntity(updateInfo));

        return result.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(result.Error);
    }

    public async Task<BaseResult> DeleteAsync(int id)
    {
        Result<Comment?> res = await repository.GetByIdAsync(id);
        if (!res.IsSuccess) return BaseResult.Failure(Error.NotFound());
        
        Result<int> result = await repository.DeleteAsync(id);
        return result.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(result.Error);
    }
}