using DomainDrivenDesign.Domain.Users;
using DomainDrivenDesign.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.WebAPI;

public static class Extensions
{
    public static async Task CreateFirstAdminUser(WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var srv = scope.ServiceProvider;

        var context = srv.GetRequiredService<ApplicationDbContext>();

        if (!context.Users.Any(p => p.IsAdmin == new IsAdmin(true)))
        {
            FirstName firstName = new("Taner");
            LastName lastName = new("Saydam");
            UserName userName = new("taner");
            Email email = new("tanersaydam@gmail.com");

            User user = User.Create(firstName, lastName, userName, email);
            IsAdmin isAdmin = new(true);
            user.SetIsAdmin(isAdmin);

            var userManager = srv.GetRequiredService<UserManager<User>>();
            IdentityResult identityResult = await userManager.CreateAsync(user, "1");
            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    Console.WriteLine(item.Description);
                }

            }
        }
    }

    public static void DatabaseMigrate(WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var srv = scope.ServiceProvider;

        var context = srv.GetRequiredService<ApplicationDbContext>();

        context.Database.Migrate();
    }
}
