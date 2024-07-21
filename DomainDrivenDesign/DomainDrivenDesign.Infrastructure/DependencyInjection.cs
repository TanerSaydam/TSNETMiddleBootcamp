using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Products;
using DomainDrivenDesign.Infrastructure.Context;
using DomainDrivenDesign.Infrastructure.Repositories;
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
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });

        services.TryAddScoped<IProductRepository, ProductRepository>();
        services.TryAddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());
        return services;
    }
}
