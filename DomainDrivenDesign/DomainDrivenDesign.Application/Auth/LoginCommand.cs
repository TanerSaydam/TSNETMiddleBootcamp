using DomainDrivenDesign.Application.Services;
using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Application.Auth;
public sealed record LoginCommand(
    string EmailOrUserName,
    string Password) : IRequest<Result<string>>;


internal sealed class LoginCommandHandler(
    SignInManager<User> signInManager,
    IJWtProvider jWtProvider
    ) : IRequestHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user =
            await signInManager.UserManager.Users
            .Where(p =>
                    p.Email == request.EmailOrUserName ||
                    p.UserName == request.EmailOrUserName)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result<string>.Failure("User not found");
        }

        SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);

        if (!signInResult.Succeeded)
        {
            if (signInResult.IsLockedOut)
            {
                return Result<string>.Failure("3 defa şifrenizi yanlış girdiğiniz için kullanıcı girişiniz 30 saniyeliğine kiltilenmiştir");
            }
            else if (signInResult.IsNotAllowed)
            {
                return Result<string>.Failure("Kullanıcı girişi yapabilmemiz için mail adresinizi onaylamalısınız");
            }
            else
            {
                return Result<string>.Failure("Şifreniz yanlış");
            }
        }

        string token = jWtProvider.CreateToken(user);

        return token;
    }
}
