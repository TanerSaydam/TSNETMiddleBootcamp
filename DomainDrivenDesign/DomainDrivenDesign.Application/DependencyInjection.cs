using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenDesign.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddFluentEmail("tanersaydam@gmail.com")
            .AddSmtpSender("localhost", 25, "username", "password");

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
        });
        return services;
    }
}
