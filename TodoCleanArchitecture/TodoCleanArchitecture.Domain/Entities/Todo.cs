namespace TodoCleanArchitecture.Domain.Entities;
public sealed class Todo
{
    public Todo()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string Work { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateOnly DeadLine { get; set; }
    public bool IsCompleted { get; set; }
}
