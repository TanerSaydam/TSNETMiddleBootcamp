using DomainDrivenDesign.Domain.Outboxes;
using DomainDrivenDesign.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Infrastructure.Repositories;
internal sealed class OutboxRepository(
    ApplicationDbContext context) : IOutboxRepository
{
    public async Task CreateAsync(OutBox outBox, CancellationToken cancellationToken = default)
    {
        await context.AddAsync(outBox, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<List<OutBox>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return context.OutBoxes.ToListAsync(cancellationToken);
    }
}
