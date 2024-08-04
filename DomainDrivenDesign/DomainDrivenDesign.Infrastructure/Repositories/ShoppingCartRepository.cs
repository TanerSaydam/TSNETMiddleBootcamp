using DomainDrivenDesign.Domain.ShoppingCarts;
using DomainDrivenDesign.Infrastructure.Context;

namespace DomainDrivenDesign.Infrastructure.Repositories;
internal sealed class ShoppingCartRepository(
    ApplicationDbContext context) : IShoppingCartRepository
{
    public async Task CreateAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        await context.AddAsync(shoppingCart, cancellationToken);
    }
}
