using DomainDrivenDesign.Application.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenDesign.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class ProductsController(
    IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await mediator.Send(request, cancellationToken);
        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        GetAllProductsQuery request = new();
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
