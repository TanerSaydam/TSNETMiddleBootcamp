namespace DomainDrivenDesign.Domain.Outboxes;

public sealed record TryCount
{
    public TryCount(int value)
    {
        if (value > 3)
        {
            throw new ArgumentOutOfRangeException("Try Count");
        }

        Value = value;
    }
    public int Value { get; private set; }
}