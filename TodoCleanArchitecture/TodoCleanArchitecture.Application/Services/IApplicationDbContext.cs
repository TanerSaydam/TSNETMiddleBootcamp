using Microsoft.EntityFrameworkCore;
using TodoCleanArchitecture.Domain.Entities;

namespace TodoCleanArchitecture.Application.Services;
public interface IApplicationDbContext
{
    public DbSet<Todo> Todos { get; set; }
}
