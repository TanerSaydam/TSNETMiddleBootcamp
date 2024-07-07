using AutoMapper;
using MediatR;
using TodoCleanArchitecture.Domain.Entities;
using TodoCleanArchitecture.Domain.Repositories;
using TS.Result;

namespace TodoCleanArchitecture.Application.Features.Todos.CreateTodo;

internal sealed class CreateTodoCommandHandler(
    ITodoRepository todoRepository,
    IMapper mapper
    ) : IRequestHandler<CreateTodoCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        bool isWorkExists = await todoRepository.AnyAsync(p => p.Work == request.Work, cancellationToken);

        if (isWorkExists)
        {
            return Result<string>.Failure(400, "This record already exsist");
        }

        Todo todo = mapper.Map<Todo>(request);
        await todoRepository.CreateAsync(todo, cancellationToken);

        return "Create is successful";
    }
}



