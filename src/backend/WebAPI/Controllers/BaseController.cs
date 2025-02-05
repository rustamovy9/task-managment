using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MobileApp.HelpersApi.ApiConventions;

namespace WebAPI.Controllers;

[ApiController]
[ApiConventionType(typeof(CustomApiConventions))]
public class BaseController : ControllerBase
{
    protected string? GetErrorMessage
    {
        get
        {
            return ModelState.IsValid
                ? null
                : string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
        }
    }
}