using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DTO_s;

public interface IBaseTaskInfo
{
    public string Title { get; init; }
    public string Description { get; init; } 
    public Status Status { get; init; }
    public Priority Priority { get; init; } 
    public DateTimeOffset DeadLine { get; init; }
    public int AssignedToUserId { get; init; }
}

public readonly record struct TaskReadInfo(
    string Title,
    string Description,
    Status Status,
    Priority Priority,
    DateTimeOffset DeadLine,
    int AssignedToUserId,
    int Id):IBaseTaskInfo;


public  record  TaskCreateInfo(
    string Title,
    string Description,
    Status Status,
    Priority Priority ,
    DateTimeOffset DeadLine,
    int AssignedToUserId) : IBaseTaskInfo;



public  record  TaskUpdateInfo(
    string Title,
    string Description,
    Status Status,
    Priority Priority ,
    DateTimeOffset DeadLine,
    int AssignedToUserId) : IBaseTaskInfo;
