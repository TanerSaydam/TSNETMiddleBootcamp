using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Users;
using DomainDrivenDesign.Domain.Users.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DomainDrivenDesign.Application.Auth;
public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password) : IRequest<Result<string>>;

internal sealed class RegisterCommandHandler(
    UserManager<User> userManager,
    IMediator mediator
    ) : IRequestHandler<RegisterCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        FirstName firstName = new(request.FirstName);
        LastName lastName = new(request.LastName);
        Domain.Users.Email email = new(request.Email);
        UserName userName = new(request.UserName);

        User user = User.Create(firstName, lastName, userName, email);

        IdentityResult identityResult = await userManager.CreateAsync(user, request.Password);
        if (!identityResult.Succeeded)
        {
            List<string> errorMessage = identityResult.Errors.Select(s => s.Description).ToList();

            return Result<string>.Failure(errorMessage);
        }

        await mediator.Publish(new RegisterDomainEvent(request.Email));


        return "User create is successful";
    }

}
