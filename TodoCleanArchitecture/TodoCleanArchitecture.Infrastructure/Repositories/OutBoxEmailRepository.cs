using TodoCleanArchitecture.Domain.Entities;
using TodoCleanArchitecture.Domain.Repositories;
using TodoCleanArchitecture.Infrastructure.Context;

namespace TodoCleanArchitecture.Infrastructure.Repositories;
internal sealed class OutBoxEmailRepository : Repository<OutBoxEmail>, IOutBoxEmailRepository
{
    public OutBoxEmailRepository(ApplicationDbContext context) : base(context)
    {
    }
}
