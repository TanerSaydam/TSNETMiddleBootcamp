using TodoCleanArchitecture.Domain.Entities;
using TodoCleanArchitecture.Domain.Repositories;
using TodoCleanArchitecture.Infrastructure.Context;

namespace TodoCleanArchitecture.Infrastructure.Repositories;
internal sealed class TodoRepository : Repository<Todo>, ITodoRepository
{
    public TodoRepository(ApplicationDbContext context) : base(context)
    {
    }
}
