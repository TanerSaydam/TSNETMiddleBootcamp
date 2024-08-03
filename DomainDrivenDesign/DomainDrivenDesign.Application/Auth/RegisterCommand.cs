using DomainDrivenDesign.Application.Services;
using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Outboxes;
using DomainDrivenDesign.Domain.Users;
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
    IOutboxRepository outboxRepository
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

        Subject subject = new("Register");
        Body body = new(EmailTemplateService.CreateRegisterBody(user.Email!));
        To to = new(user.Email!);
        OutBox outBox = new(to, subject, body);

        await outboxRepository.CreateAsync(outBox, cancellationToken);

        return "User create is successful";
    }

}
