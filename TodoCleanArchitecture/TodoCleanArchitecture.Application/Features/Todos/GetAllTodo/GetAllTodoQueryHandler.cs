using MediatR;
using TodoCleanArchitecture.Domain.Entities;
using TodoCleanArchitecture.Domain.Repositories;

namespace TodoCleanArchitecture.Application.Features.Todos.GetAllTodo;

internal sealed class GetAllTodoQueryHandler(
    ITodoRepository todoRepository) : IRequestHandler<GetAllTodoQuery, List<Todo>>
{
    public async Task<List<Todo>> Handle(GetAllTodoQuery request, CancellationToken cancellationToken)
    {
        var response = await todoRepository.GetAllAsync(cancellationToken);
        return response;
    }
}
