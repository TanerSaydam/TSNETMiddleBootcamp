using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoCleanArchitecture.Application.Services;
using TodoCleanArchitecture.Domain.Entities;
using TodoCleanArchitecture.Domain.Repositories;

namespace TodoCleanArchitecture.Application.Features.Todos.GetAllTodo;

internal sealed class GetAllTodoQueryHandler(
    ITodoRepository todoRepository,
    ICacheService cache
    //IApplicationDbContext dbContext
    ) : IRequestHandler<GetAllTodoQuery, List<Todo>>
{
    public async Task<List<Todo>> Handle(GetAllTodoQuery request, CancellationToken cancellationToken)
    {
        //var result = dbContext.Todos.ToList();

        cache.TryGetValue("todos", out List<Todo>? todos);

        if (todos is null)
        {
            todos = await todoRepository.GetAll().Take(10).ToListAsync(cancellationToken);

            cache.Set("todos", todos);
        }

        return todos;
    }
}
