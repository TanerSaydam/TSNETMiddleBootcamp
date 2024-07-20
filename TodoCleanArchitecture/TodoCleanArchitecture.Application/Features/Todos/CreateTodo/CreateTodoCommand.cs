using MediatR;
using TodoCleanArchitecture.Domain.Abstractions;

namespace TodoCleanArchitecture.Application.Features.Todos.CreateTodo;
public sealed record CreateTodoCommand(
    string Work,
    string Email,
    DateOnly DeadLine) : IRequest<Result<string>>;



