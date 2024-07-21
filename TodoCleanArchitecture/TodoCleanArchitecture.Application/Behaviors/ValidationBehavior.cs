using FluentValidation;
using MediatR;
using System.Text.Json;

namespace TodoCleanArchitecture.Application.Behaviors;
public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var errors = _validators
            .Select(s => s.Validate(context))
            .SelectMany(s => s.Errors)
            .Where(s => s != null)
            .Select(s => s.ErrorMessage)
            .ToList();

        if (errors.Any())
        {
            string errorMessage = JsonSerializer.Serialize(errors);
            throw new ValidationException(errorMessage);
        }

        return await next();
    }
}
