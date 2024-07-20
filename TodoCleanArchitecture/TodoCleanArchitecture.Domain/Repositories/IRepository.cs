using System.Linq.Expressions;

namespace TodoCleanArchitecture.Domain.Repositories;
public interface IRepository<T>
{
    Task CreateAsync(T data, CancellationToken cancellationToken = default);
    void Update(T data);
    Task DeleteAsync(T data, CancellationToken cancellationToken = default);
    IQueryable<T> GetAll();
    IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}
