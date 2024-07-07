using MediatR;

namespace TodoCleanArchitecture.Application.Features.Todos.DeleteTodoById;
public sealed record DeleteTodoByIdCommand(
    Guid Id) : IRequest;
