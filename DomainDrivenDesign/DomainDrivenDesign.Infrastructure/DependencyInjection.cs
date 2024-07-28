using DomainDrivenDesign.Application.Services;
using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Products;
using DomainDrivenDesign.Domain.Users;
using DomainDrivenDesign.Infrastructure.Context;
using DomainDrivenDesign.Infrastructure.Options;
using DomainDrivenDesign.Infrastructure.Repositories;
using DomainDrivenDesign.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DomainDrivenDesign.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.Configure<Jwt>(configuration.GetSection("JWT"));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });

        services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 1;

            options.SignIn.RequireConfirmedEmail = true;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);

        })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.TryAddScoped<IProductRepository, ProductRepository>();
        services.TryAddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());
        services.TryAddTransient<IJWtProvider, JwtProvider>();
        return services;
    }
}
