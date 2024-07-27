namespace DomainDrivenDesign.Domain.Shared;

public sealed record Identity //Value object
{
    public Guid Value { get; init; }
    public Identity(Guid value)
    {
        //kontroller
        Value = value;
    }
}
