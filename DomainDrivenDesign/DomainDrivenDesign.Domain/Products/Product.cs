using DomainDrivenDesign.Domain.Shared;
using DomainDrivenDesign.Domain.ShoppingCarts;

namespace DomainDrivenDesign.Domain.Products;
public sealed class Product
{
    private Product()
    {

    }
    public Product(Identity id, Name name, Description description, Price price, Stock stock)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        IsDelete = new(false);
    }

    public Identity Id { get; private set; } = default!;
    public Name Name { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public Price Price { get; private set; } = default!;
    public Stock Stock { get; private set; } = default!;
    public IsDelete IsDelete { get; private set; } = default!;
    public List<ShoppingCart>? ShoppingCarts { get; private set; }
    public void SetName(Name name)
    {
        Name = name;
    }

    public void SetDescription(Description description)
    {
        Description = description;
    }

    public void SetPrice(Price price)
    {
        Price = price;
    }

    public void SetStock(Stock stock)
    {
        Stock = stock;
    }

    public void SetDelete(IsDelete isDelete)
    {
        IsDelete = isDelete;
    }
}
