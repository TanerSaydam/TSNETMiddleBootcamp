using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoCleanArchitecture.Application.Features.Todos.CreateTodo;
using TodoCleanArchitecture.Application.Features.Todos.DeleteTodoById;
using TodoCleanArchitecture.Application.Features.Todos.GetAllTodo;
using TodoCleanArchitecture.Application.Features.Todos.UpdateTodo;
using TodoCleanArchitecture.WebAPI.AOP;

namespace TodoCleanArchitecture.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class TodosController(
    IMediator mediator) : ControllerBase
{
    [HttpGet]
    [EnableQueryWithMetadata]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        GetAllTodoQuery request = new();
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        DeleteTodoByIdCommand request = new(id);
        await mediator.Send(request, cancellationToken);
        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        await mediator.Send(request, cancellationToken);
        return Created();
    }
}