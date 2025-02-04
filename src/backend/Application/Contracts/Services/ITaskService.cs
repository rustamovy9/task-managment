

using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;

namespace Application.Contracts.Services;

public interface ITaskService
{
    Task<Result<PagedResponse<IEnumerable<TaskReadInfo>>>> GetAllAsync(TaskFilter filter);
    Task<Result<TaskReadInfo>> GetByIdAsync(int id);
    Task<BaseResult> CreateAsync(TaskCreateInfo createInfo);
    Task<BaseResult> UpdateAsync(int id,TaskUpdateInfo updateInfo);
    Task<BaseResult> DeleteAsync(int id);
}   