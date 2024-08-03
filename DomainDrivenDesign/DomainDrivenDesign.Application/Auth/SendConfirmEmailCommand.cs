using DomainDrivenDesign.Application.Services;
using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Outboxes;
using DomainDrivenDesign.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DomainDrivenDesign.Application.Auth;
public sealed record SendConfirmEmailCommand(string Email) : IRequest<Result<string>>;

internal sealed class SendConfirmEmailCommandHandler(
    UserManager<User> userManager,
    IOutboxRepository outboxRepository) : IRequestHandler<SendConfirmEmailCommand, Result<string>>
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

        Subject subject = new("Register");
        Body body = new(EmailTemplateService.CreateRegisterBody(user.Email!));
        To to = new(user.Email!);
        OutBox outBox = new(to, subject, body);

        await outboxRepository.CreateAsync(outBox, cancellationToken);

        return "Confirm email send";
    }
}
