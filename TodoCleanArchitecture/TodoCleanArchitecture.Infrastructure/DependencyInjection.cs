using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TodoCleanArchitecture.Domain.Repositories;
using TodoCleanArchitecture.Infrastructure.Context;
using TodoCleanArchitecture.Infrastructure.Repositories;

namespace TodoCleanArchitecture.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.TryAddScoped<ApplicationDbContext>();
        services.TryAddScoped<ITodoRepository, TodoRepository>();
        return services;
    }
}
