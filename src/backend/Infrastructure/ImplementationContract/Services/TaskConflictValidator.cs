using Application.Contracts.Services;
using Application.DTO_s;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ImplementationContract.Services;

public sealed class TaskConflictValidator(DataContext context) : ITaskConflictValidator
{
    public async Task<BaseResult> CheckForConflictAsync(TaskCreateInfo createInfo)
    {
        return await ValidateCommonConflictsAsync(
            title: createInfo.Title,
            assignedToUserId: createInfo.AssignedToUserId,
            status: createInfo.Status,
            deadline: createInfo.DeadLine,
            taskId: null  // ID отсутствует при создании
        );
    }

    public async Task<BaseResult> CheckForConflictAsync(int taskId,TaskUpdateInfo updateInfo)
    {
        return await ValidateCommonConflictsAsync(
            title: updateInfo.Title,
            assignedToUserId: updateInfo.AssignedToUserId,
            status: updateInfo.Status,
            deadline: updateInfo.DeadLine,
            taskId: taskId // ID есть при обновлении
        );
    }

    public async Task<BaseResult>  ValidateCommonConflictsAsync(string title, int assignedToUserId, Status status, DateTimeOffset deadline, int? taskId)
    {
        // Проверка на дубликат заголовка (исключая текущую задачу при обновлении)
        bool isTitleDuplicate = await context.Tasks.AnyAsync(t => t.Title == title && (taskId == null || t.Id != taskId));
        if (isTitleDuplicate)
            return BaseResult.Failure(Error.Conflict("Задача с таким заголовком уже существует."));

        // Проверка, существует ли назначенный пользователь
        bool userExists = await context.Users.AnyAsync(u => u.Id == assignedToUserId);
        if (!userExists)
            return BaseResult.Failure(Error.Conflict("Назначенный пользователь не найден."));

        // Проверка на недопустимые статусы
        if (status is Status.Completed or Status.Cancelled)
            return BaseResult.Failure(Error.Conflict("Нельзя создать или обновить задачу со статусом 'Completed' или 'Cancelled'."));

        // Проверка дедлайна
        if (deadline <= DateTimeOffset.UtcNow)
            return BaseResult.Failure(Error.Conflict("Дедлайн не может быть в прошлом."));

        return BaseResult.Success();
    }
}
