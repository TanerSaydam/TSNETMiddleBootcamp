using AutoMapper;
using MediatR;
using TodoCleanArchitecture.Application.Services;
using TodoCleanArchitecture.Domain.Entities;
using TodoCleanArchitecture.Domain.Repositories;

namespace TodoCleanArchitecture.Application.Features.Todos.UpdateTodo;

internal sealed class UpdateTodoCommandHandler(
    ITodoRepository todoRepository,
    IMapper mapper,
    ICacheService cache,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateTodoCommand>
{
    public async Task Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        Todo? todo = await todoRepository.GetByIdAsync(request.Id, cancellationToken);
        if (todo is null)
        {
            throw new ArgumentNullException("Todo not found");
        }

        mapper.Map(request, todo);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        cache.Remove("todos");
    }
}
