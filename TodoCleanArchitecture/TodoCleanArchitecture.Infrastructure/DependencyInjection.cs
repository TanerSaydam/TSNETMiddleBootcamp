using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using TodoCleanArchitecture.Application.Services;
using TodoCleanArchitecture.Domain.Repositories;
using TodoCleanArchitecture.Infrastructure.Context;
using TodoCleanArchitecture.Infrastructure.Options;
using TodoCleanArchitecture.Infrastructure.Repositories;
using TodoCleanArchitecture.Infrastructure.Services;

namespace TodoCleanArchitecture.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IOptionsMonitor<ConnectionStringOptions> conOptions, IConfiguration configuration)
    {
        //string? con = configuration.GetConnectionString("MongoDb");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            //Console.WriteLine(con);
            options.UseSqlServer(conOptions.CurrentValue.SqlServer);
        });
        services.TryAddScoped<ITodoRepository, TodoRepository>();
        services.TryAddScoped<ICacheService, MemoryCacheService>();
        services.TryAddScoped<IOutBoxEmailRepository, OutBoxEmailRepository>();
        services.TryAddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.TryAddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());
        return services;
    }
}
