using DomainDrivenDesign.Domain.Products;
using MediatR;

namespace DomainDrivenDesign.Application.Products;
public sealed record GetAllProductsQuery() : IRequest<List<GetAllProductsQueryResponse>>;

internal sealed class GetAllProductsQueryHandler(
    IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, List<GetAllProductsQueryResponse>>
{
    public async Task<List<GetAllProductsQueryResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await productRepository.GetAllAsync(cancellationToken);

        var response = result.Select(s => new GetAllProductsQueryResponse(
            s.Id.Value,
            s.Name.Value,
            s.Description.Value,
            s.Price.Value,
            s.Stock.Value)).ToList();

        return response;
    }
}


public sealed record GetAllProductsQueryResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Stock);