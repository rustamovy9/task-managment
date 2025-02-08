using Application.Contracts.Services;
using Application.DTO_s;
using Application.Filters;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.HelpersApi.Extensions.ResultPattern;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/task-histories")]
[Authorize]
public class TaskHistoryController(ITaskHistoryService service) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] TaskHistoryFilter filter)
        => (await service.GetAllAsync(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
        => (await service.GetByIdAsync(id)).ToActionResult();

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskHistoryCreateInfo entity)
        => (await service.CreateAsync(entity)).ToActionResult();

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TaskHistoryUpdateInfo entity)
        => (await service.UpdateAsync(id, entity)).ToActionResult();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
        => (await service.DeleteAsync(id)).ToActionResult();
}