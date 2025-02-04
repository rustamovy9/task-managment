using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;

namespace Application.Contracts.Services;

public interface ICommentService
{
    Task<Result<PagedResponse<IEnumerable<CommentReadInfo>>>> GetAllAsync(CommentFilter filter);
    Task<Result<CommentReadInfo>> GetByIdAsync(int id);
    Task<BaseResult> CreateAsync(CommentCreateInfo createInfo);
    Task<BaseResult> UpdateAsync(int id,CommentUpdateInfo updateInfo);
    Task<BaseResult> DeleteAsync(int id);
}