using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Shared;
using DomainDrivenDesign.Domain.ShoppingCarts;
using MediatR;

namespace DomainDrivenDesign.Application.ShoppingCarts;
public sealed record CreateShoppingCartCommand(
    Guid ProductId,
    int Quantity) : IRequest<Result<string>>;

internal sealed class CreateShoppingCartCommandHandler(
    IShoppingCartRepository shoppingCartRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateShoppingCartCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        Identity productId = new(request.ProductId);
        Quantity quantity = new(request.Quantity);

        ShoppingCart shoppingCart = new(productId, quantity);

        await shoppingCartRepository.CreateAsync(shoppingCart, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "The product has been successfully added to cart";
    }
}

