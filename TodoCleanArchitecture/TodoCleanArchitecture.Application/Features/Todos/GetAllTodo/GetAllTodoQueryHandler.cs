using MediatR;
using TodoCleanArchitecture.Domain.Entities;
using TodoCleanArchitecture.Domain.Repositories;

namespace TodoCleanArchitecture.Application.Features.Todos.GetAllTodo;

internal sealed class GetAllTodoQueryHandler(
    ITodoRepository todoRepository) : IRequestHandler<GetAllTodoQuery, IQueryable<Todo>>
{
    public async Task<IQueryable<Todo>> Handle(GetAllTodoQuery request, CancellationToken cancellationToken)
    {
        var response = todoRepository.GetAll();
        await Task.CompletedTask;
        return response;
    }
}
