using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Products;
using MediatR;

namespace DomainDrivenDesign.Application.Products;
public sealed record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int Stock) : IRequest;

internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateProductCommand>
{
    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool isNameExists = await productRepository.IsNameExists(request.Name, cancellationToken);
        if (isNameExists)
        {
            throw new ArgumentException("Product name already exists");
        }

        Identity identity = new(Guid.NewGuid());
        Name name = Name.Create(request.Name);
        Description description = new(request.Description);
        Price price = new(request.Price);
        Stock stock = new(request.Stock);


        Product product = new(identity, name, description, price, stock);

        await productRepository.CreateAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
