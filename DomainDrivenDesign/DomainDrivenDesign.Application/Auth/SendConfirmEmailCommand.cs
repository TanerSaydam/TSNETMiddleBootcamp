using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Users;
using DomainDrivenDesign.Domain.Users.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DomainDrivenDesign.Application.Auth;
public sealed record SendConfirmEmailCommand(string Email) : IRequest<Result<string>>;

internal sealed class SendConfirmEmailCommandHandler(
    UserManager<User> userManager,
    IMediator mediator) : IRequestHandler<SendConfirmEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result<string>.Failure("User not found");
        }

        if (user.EmailConfirmed)
        {
            return Result<string>.Failure("User email already confirmed");
        }

        await mediator.Publish(new RegisterDomainEvent(request.Email));

        return "Confirm email send";
    }
}
