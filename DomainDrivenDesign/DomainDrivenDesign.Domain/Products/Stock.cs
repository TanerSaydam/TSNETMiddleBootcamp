namespace DomainDrivenDesign.Domain.Products;

public sealed record Stock
{
    public int Value { get; init; }
    public Stock(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("Stock must be greater then 0");
        }

        Value = value;
    }
}