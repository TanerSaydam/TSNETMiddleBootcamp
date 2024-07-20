using AutoMapper;
using MediatR;
using TodoCleanArchitecture.Application.Services;
using TodoCleanArchitecture.Domain.Abstractions;
using TodoCleanArchitecture.Domain.Entities;
using TodoCleanArchitecture.Domain.Events;
using TodoCleanArchitecture.Domain.Repositories;

namespace TodoCleanArchitecture.Application.Features.Todos.CreateTodo;

internal sealed class CreateTodoCommandHandler(
    ITodoRepository todoRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IOutBoxEmailRepository outBoxEmailRepository,
    ICacheService cache,
    IMediator mediator
    //IApplicationDbContext dbContext
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

        OutBoxEmail outBoxEmail = new()
        {
            TodoId = todo.Id,
            IsSuccesful = true //bu false olmalı, denememiz bittiği için true işaretledik
        };


        await outBoxEmailRepository.CreateAsync(outBoxEmail, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        cache.Remove("todos");

        await mediator.Publish(new TodoDomainEvent(todo));

        //TodoService.SendEmail();
        //TodoService.SendSms();

        return "Create is successful";
    }
}



