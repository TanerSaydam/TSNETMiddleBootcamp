using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TodoCleanArchitecture.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            //configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
