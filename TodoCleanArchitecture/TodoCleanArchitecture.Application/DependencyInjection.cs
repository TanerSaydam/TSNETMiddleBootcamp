using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TodoCleanArchitecture.Application.Behaviors;
using TodoCleanArchitecture.Domain.Abstractions;

namespace TodoCleanArchitecture.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddFluentEmail("tanersaydam@gmail.com")
            .AddSmtpSender("localhost", 2525);

        services.AddMemoryCache();
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(Entity).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}
