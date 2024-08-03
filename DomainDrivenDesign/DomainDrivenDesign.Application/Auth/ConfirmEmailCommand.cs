using DomainDrivenDesign.Application.Services;
using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Outboxes;
using DomainDrivenDesign.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DomainDrivenDesign.Application.Auth;
public sealed record ConfirmEmailCommand(
    string Email) : IRequest<Result<string>>;

internal sealed class ConfirmEmailCommandHandler(
    UserManager<User> userManager,
    IOutboxRepository outboxRepository) : IRequestHandler<ConfirmEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
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

        user.EmailConfirmed = true;

        IdentityResult identityResult = await userManager.UpdateAsync(user);
        if (!identityResult.Succeeded)
        {
            List<string> errorMessage = identityResult.Errors.Select(s => s.Description).ToList();

            return Result<string>.Failure(errorMessage);
        }


        Subject subject = new("Email Confirmed");
        Body body = new(EmailTemplateService.CreateAfterConfirmEmailBody());
        To to = new(user.Email!);
        OutBox outBox = new(to, subject, body);

        await outboxRepository.CreateAsync(outBox, cancellationToken);

        return "Email confirmed";
    }
}
