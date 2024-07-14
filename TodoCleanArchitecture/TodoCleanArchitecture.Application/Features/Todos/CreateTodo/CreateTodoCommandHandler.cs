using AutoMapper;
using MediatR;
using TodoCleanArchitecture.Application.Services;
using TodoCleanArchitecture.Domain.Abstractions;
using TodoCleanArchitecture.Domain.Entities;
using TodoCleanArchitecture.Domain.Repositories;

namespace TodoCleanArchitecture.Application.Features.Todos.CreateTodo;

internal sealed class CreateTodoCommandHandler(
    ITodoRepository todoRepository,
    IMapper mapper,
    ICacheService cache
    ) : IRequestHandler<CreateTodoCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        bool isWorkExists = await todoRepository.AnyAsync(p => p.Work == request.Work, cancellationToken);

        if (isWorkExists)
        {
            var errorResponse = Result<string>.Failure(500, "This record already exsist");
            return errorResponse;
        }

        Todo todo = mapper.Map<Todo>(request);
        await todoRepository.CreateAsync(todo, cancellationToken);

        cache.Remove("todos");
        return "Create is successful";
    }
}



