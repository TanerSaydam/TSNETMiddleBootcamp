using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenDesign.Infrastructure;

public static class ServiceTool
{
    public static IServiceProvider ServiceProvider { get; set; } = default!;
    public static IServiceCollection CreateServiceTool(this IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();
        return services;
    }
}
