namespace DomainDrivenDesign.Domain.Products;

public sealed record Price
{
    public decimal Value { get; init; }
    public Price(decimal value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("Price must be greater then 0");
        }

        Value = value;
    }
}
