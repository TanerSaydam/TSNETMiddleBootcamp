using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Products;
using DomainDrivenDesign.Domain.Shared;
using MediatR;

namespace DomainDrivenDesign.Application.Products;
public sealed record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int Stock) : IRequest<Result<string>>;

internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool isNameExists = await productRepository.IsNameExists(request.Name, cancellationToken);
        if (isNameExists)
        {
            return Result<string>.Failure("Product name already exists", 400);
        }

        Identity identity = new(Guid.NewGuid());
        Name name = Name.Create(request.Name);
        Description description = new(request.Description);
        Price price = new(request.Price);
        Stock stock = new(request.Stock);


        Product product = new(identity, name, description, price, stock);

        await productRepository.CreateAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Create is successful";
    }
}
