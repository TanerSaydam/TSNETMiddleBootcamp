namespace DomainDrivenDesign.Domain.Products;

public sealed record Identity //Value object
{
    public Guid Value { get; init; }
    public Identity(Guid value)
    {
        //kontroller
        Value = value;
    }
}
