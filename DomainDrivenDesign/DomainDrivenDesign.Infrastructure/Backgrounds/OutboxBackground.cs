using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Outboxes;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DomainDrivenDesign.Infrastructure.Backgrounds;
public sealed class OutboxBackground : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            var srv = ServiceTool.ServiceProvider;
            using (var scope = srv.CreateScope())
            {
                var outboxRepository = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var fluentEmail = scope.ServiceProvider.GetRequiredService<IFluentEmail>();

                List<OutBox> outBoxes = await outboxRepository.GetAllAsync(stoppingToken);

                foreach (var item in outBoxes)
                {
                    SendResponse sendResponse =
                        await fluentEmail
                        .To(item.To.Value)
                        .Subject(item.Subject.Value)
                        .Body(item.Body.Value)
                        .SendAsync(stoppingToken);

                    if (!sendResponse.Successful)
                    {
                        item.SetTryCount();
                        await unitOfWork.SaveChangesAsync(stoppingToken);
                    }
                    else
                    {
                        item.SetIsSend();
                        await unitOfWork.SaveChangesAsync(stoppingToken);
                    }

                    await Task.Delay(100);
                }
            }

            await Task.Delay(10000);
        }
    }
}
