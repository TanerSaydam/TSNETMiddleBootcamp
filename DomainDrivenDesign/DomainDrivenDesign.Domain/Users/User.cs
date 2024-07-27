using Microsoft.AspNetCore.Identity;

namespace DomainDrivenDesign.Domain.Users;
public sealed class User : IdentityUser<Guid>
{
    private User()
    {

    }
    private User(
        FirstName firstName,
        LastName lastName,
        UserName userName,
        Email email)
    {
        Id = Guid.NewGuid();
        UserName = userName.Value;
        Email = email.Value;
        FirstName = firstName;
        LastName = lastName;
    }
    public FirstName FirstName { get; private set; } = default!;
    public LastName LastName { get; private set; } = default!;
    public IsAdmin IsAdmin { get; private set; } = default!;
    public static User Create(
        FirstName firstName,
        LastName lastName,
        UserName userName,
        Email email)
    {
        return new(firstName, lastName, userName, email);
    }

    public void SetFirstName(FirstName firstName)
    {
        FirstName = firstName;
    }

    public void SetLastName(LastName lastName)
    {
        LastName = lastName;
    }

    public void SetUserName(UserName userName)
    {
        UserName = userName.Value;
    }

    public void SetEmail(Email email)
    {
        UserName = email.Value;
    }

    public void SetIsAdmin(IsAdmin isAdmin)
    {
        IsAdmin = isAdmin;
    }
}
