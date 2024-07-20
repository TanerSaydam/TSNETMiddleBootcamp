using TodoCleanArchitecture.Domain.Abstractions;

namespace TodoCleanArchitecture.Domain.Entities;
public sealed class OutBoxEmail : Entity
{
    public bool IsSuccesful { get; set; }
    public int TryCount { get; set; }
    public Guid TodoId { get; set; }
    public Todo? Todo { get; set; }
}
