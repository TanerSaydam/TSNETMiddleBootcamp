using MediatR;

namespace TodoCleanArchitecture.Application.Features.Todos.UpdateTodo;
public sealed record UpdateTodoCommand(
    Guid Id,
    string Work,
    DateOnly DeadLine,
    bool IsCompleted) : IRequest;
