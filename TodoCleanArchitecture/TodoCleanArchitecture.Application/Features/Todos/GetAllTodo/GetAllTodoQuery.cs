using MediatR;
using TodoCleanArchitecture.Domain.Entities;

namespace TodoCleanArchitecture.Application.Features.Todos.GetAllTodo;
public sealed record GetAllTodoQuery : IRequest<List<Todo>>;
