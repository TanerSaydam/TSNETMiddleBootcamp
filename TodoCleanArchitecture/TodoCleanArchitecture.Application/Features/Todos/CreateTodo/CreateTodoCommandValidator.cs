using FluentValidation;

namespace TodoCleanArchitecture.Application.Features.Todos.CreateTodo;
public sealed class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(p => p.Work).MinimumLength(3).NotEmpty().NotNull();
    }
}
