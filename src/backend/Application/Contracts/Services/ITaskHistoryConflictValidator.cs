using Application.DTO_s;
using Application.Extensions.ResultPattern;
using Domain.Entities;
using Domain.Enums;

namespace Application.Contracts.Services;

public interface ITaskHistoryConflictValidator
{
    Task<BaseResult> ValidateTaskHistoryAsync(TaskHistoryCreateInfo request);
    Task<BaseResult> ValidateTaskHistoryAsync(TaskHistoryUpdateInfo request);
}