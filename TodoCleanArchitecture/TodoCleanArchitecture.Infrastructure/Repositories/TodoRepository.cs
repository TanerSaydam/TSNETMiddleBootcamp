using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TodoCleanArchitecture.Domain.Entities;
using TodoCleanArchitecture.Domain.Repositories;
using TodoCleanArchitecture.Infrastructure.Context;

namespace TodoCleanArchitecture.Infrastructure.Repositories;
internal sealed class TodoRepository(
    ApplicationDbContext context) : ITodoRepository
{
    public async Task<bool> AnyAsync(Expression<Func<Todo, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await context.Todos.AnyAsync(predicate, cancellationToken);
    }

    public async Task CreateAsync(Todo todo, CancellationToken cancellationToken = default)
    {
        await context.AddAsync(todo, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Todo todo, CancellationToken cancellationToken = default)
    {
        context.Remove(todo);
        await context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<Todo> GetAll()
    {
        return context.Todos.AsQueryable();
    }

    public async Task<List<Todo>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Todos.ToListAsync(cancellationToken);
    }

    public async Task<Todo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Todos.FindAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(Todo todo, CancellationToken cancellationToken = default)
    {
        context.Update(todo);
        await context.SaveChangesAsync(cancellationToken);
    }
}
