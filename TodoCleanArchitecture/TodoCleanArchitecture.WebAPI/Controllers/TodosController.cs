using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoCleanArchitecture.Application.Features.Todos.GetAllTodo;

namespace TodoCleanArchitecture.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class TodosController(
    IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        GetAllTodoQuery request = new();
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
