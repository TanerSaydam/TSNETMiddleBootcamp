using DomainDrivenDesign.Application.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenDesign.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class AuthController(
    IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
