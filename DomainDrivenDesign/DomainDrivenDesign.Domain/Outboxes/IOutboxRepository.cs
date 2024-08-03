namespace DomainDrivenDesign.Domain.Outboxes;
public interface IOutboxRepository
{
    Task CreateAsync(OutBox outBox, CancellationToken cancellationToken = default);
    Task<List<OutBox>> GetAllAsync(CancellationToken cancellationToken = default);
}
