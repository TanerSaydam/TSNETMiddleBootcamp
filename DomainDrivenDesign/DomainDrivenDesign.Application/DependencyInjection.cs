using DomainDrivenDesign.Domain.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace DomainDrivenDesign.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var srv = services.BuildServiceProvider();
        StmpOptions smtpOptions = srv.GetRequiredService<IOptions<StmpOptions>>().Value;

        SmtpClient smtpClient = new()
        {
            Host = smtpOptions.Host,
            Port = smtpOptions.Port,
            Credentials =
                (string.IsNullOrEmpty(smtpOptions.UserName) ?
                null :
                new NetworkCredential(smtpOptions.UserName, smtpOptions.Password))
        };

        services.AddFluentEmail("tanersaydam@gmail.com")
            .AddSmtpSender(smtpClient);

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
        });
        return services;
    }
}
