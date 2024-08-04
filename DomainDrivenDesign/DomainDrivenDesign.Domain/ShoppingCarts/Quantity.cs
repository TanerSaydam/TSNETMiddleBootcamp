namespace DomainDrivenDesign.Domain.ShoppingCarts;

public sealed record Quantity
{
    public int Value { get; set; }

    public Quantity(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("Quantity must be greater then 0");
        }
        Value = value;
    }
}