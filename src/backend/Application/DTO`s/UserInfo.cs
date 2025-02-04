using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DTO_s;

public interface IBaseUserInfo
{
    public string UserName { get; init; }
    public string FirstName { get; init; } 
    public string LastName { get; init; }
    public DateTimeOffset DateOfBirth { get; init; }
    public string Email { get; init; } 
    public string? PhoneNumber { get; init; } 
    public string? Address { get; init; } 
}

public readonly record struct UserReadInfo(
    string UserName,
    string FirstName,
    string LastName,
    DateTimeOffset DateOfBirth,
    string Email,
    string? PhoneNumber,
    string? Address,
    string UserImageName,    
    int Id):IBaseUserInfo;

public  record  UserUpdateInfo(
    string UserName,
    string FirstName,
    string LastName,
    DateTimeOffset DateOfBirth,
    string Email,
    string? PhoneNumber,
    string? Address,
    IFormFile? File) : IBaseUserInfo;
