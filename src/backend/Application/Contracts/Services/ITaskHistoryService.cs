using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;

namespace Application.Contracts.Services;

public interface ITaskHistoryService
{
    Task<Result<PagedResponse<IEnumerable<TaskHistoryReadInfo>>>> GetAllAsync(TaskHistoryFilter filter);
    Task<Result<TaskHistoryReadInfo>> GetByIdAsync(int id);
    Task<BaseResult> CreateAsync(TaskHistoryCreateInfo createInfo);
    Task<BaseResult> UpdateAsync(int id, TaskHistoryUpdateInfo updateInfo);
    Task<BaseResult> DeleteAsync(int id);
}
