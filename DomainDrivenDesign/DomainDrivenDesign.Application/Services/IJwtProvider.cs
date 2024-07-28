using DomainDrivenDesign.Domain.Users;

namespace DomainDrivenDesign.Application.Services;
public interface IJWtProvider
{
    string CreateToken(User user);
}
