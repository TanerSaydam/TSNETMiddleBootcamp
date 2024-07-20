using MediatR;

namespace TodoCleanArchitecture.Application.Features.Todos.UpdateTodo;
public sealed record UpdateTodoCommand(
    Guid Id,
    string Work,
    string Email,
    DateOnly DeadLine,
    bool IsCompleted) : IRequest;
