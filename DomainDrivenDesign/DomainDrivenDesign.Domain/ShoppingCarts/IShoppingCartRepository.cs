namespace DomainDrivenDesign.Domain.ShoppingCarts;
public interface IShoppingCartRepository
{
    Task CreateAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default);
}
