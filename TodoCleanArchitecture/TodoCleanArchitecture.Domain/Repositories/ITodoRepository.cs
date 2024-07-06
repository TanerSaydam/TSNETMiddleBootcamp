using System.Linq.Expressions;
using TodoCleanArchitecture.Domain.Entities;

namespace TodoCleanArchitecture.Domain.Repositories;
public interface ITodoRepository
{
    Task CreateAsync(Todo todo, CancellationToken cancellationToken = default);
    Task UpdateAsync(Todo todo, CancellationToken cancellationToken = default);
    Task DeleteAsync(Todo todo, CancellationToken cancellationToken = default);
    Task<List<Todo>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Todo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<Todo, bool>> predicate, CancellationToken cancellationToken = default);
    IQueryable<Todo> GetAll();
}
