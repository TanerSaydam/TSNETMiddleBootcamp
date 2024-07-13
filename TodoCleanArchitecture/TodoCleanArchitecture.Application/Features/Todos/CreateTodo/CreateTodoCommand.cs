using MediatR;
using TodoCleanArchitecture.Domain.Abstractions;

namespace TodoCleanArchitecture.Application.Features.Todos.CreateTodo;
public sealed record CreateTodoCommand(
    string Work,
    DateOnly DeadLine) : IRequest<Result<string>>;



