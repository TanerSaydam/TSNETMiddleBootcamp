using DomainDrivenDesign.Domain.Products;
using DomainDrivenDesign.Domain.Shared;

namespace DomainDrivenDesign.Domain.ShoppingCarts;
public sealed class ShoppingCart
{
    private ShoppingCart()
    {
    }
    public ShoppingCart(Identity productId, Quantity quantity)
    {
        Id = Identity.Create();
        ProductId = productId;
        Quantity = quantity;
        CreatedAt = new(DateTime.Now);
    }
    public Identity Id { get; private set; } = default!;
    public Identity ProductId { get; private set; } = default!;
    public Product? Product { get; private set; }
    public Quantity Quantity { get; private set; } = default!;
    public CreatedAt CreatedAt { get; private set; } = default!;

    public void SetProductId(Identity productId)
    {
        ProductId = productId;
    }

    public void SetQuantity(Quantity quantity)
    {
        Quantity = quantity;
    }
}