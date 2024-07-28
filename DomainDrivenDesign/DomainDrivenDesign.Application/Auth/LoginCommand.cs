using DomainDrivenDesign.Application.Services;
using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Users;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Application.Auth;
public sealed record LoginCommand(
    string EmailOrUserName,
    string Password) : IRequest<Result<string>>;

internal sealed class LoginCommandHandler(
    SignInManager<User> signInManager,
    IJWtProvider jWtProvider,
    IFluentEmail fluentEmail
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


        SendResponse sendResponse = await fluentEmail.To(user.Email).Subject("Kullanıcı girişi").Body(@"
<h1>Kullanıcı giriş yaptı</h1>
<br>
<b>Selam bu bir test mailidir. Kullanıcı girişinden hemen sonra gönderilmiştir. İstersen aşağıdaki butona tıkla</b>
<button>Beni tıkla</button>"
).SendAsync(cancellationToken);

        if (sendResponse.Successful)
        {

        }

        return token;
    }
}
