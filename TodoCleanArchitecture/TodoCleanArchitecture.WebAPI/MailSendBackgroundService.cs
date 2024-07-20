using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.EntityFrameworkCore;
using TodoCleanArchitecture.Domain.Entities;
using TodoCleanArchitecture.Domain.Repositories;
using TodoCleanArchitecture.WebAPI;

namespace TodoCleanArchitecture.Application.Services;
public sealed class MailSendBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var services = ServiceTool.ServiceProvider;

        IOutBoxEmailRepository outBoxEmailRepository = services.GetRequiredService<IOutBoxEmailRepository>();
        IFluentEmail fluentEmail = services.GetRequiredService<IFluentEmail>();
        IUnitOfWork unitOfWork = services.GetRequiredService<IUnitOfWork>();

        while (!stoppingToken.IsCancellationRequested)
        {
            List<OutBoxEmail> outBoxEmails =
                await outBoxEmailRepository
                .Where(p => p.IsSuccesful == false && p.TryCount < 3)
                .Include(p => p.Todo)
                .ToListAsync(stoppingToken);

            foreach (var item in outBoxEmails)
            {
                try
                {
                    SendResponse sendResponse = await fluentEmail
                    .To(item.Todo!.Email)
                    .Subject("New Task")
                    .Body($"<h1><b>{item.Todo.Work}</b></h1>")
                    .SendAsync(stoppingToken);

                    if (sendResponse.Successful)
                    {
                        item.IsSuccesful = true;

                    }
                    else
                    {
                        item.TryCount++;
                        if (item.TryCount == 3)
                        {
                            await fluentEmail
                            .To("tanersaydam@gmail.com")
                            .Subject("Send Task Is Failed")
                            .Body($"<h1><b>We cannot available the send email this task:<br>Email: {item.Todo.Email}<br>Work: {item.Todo.Work} </b></h1>")
                            .SendAsync(stoppingToken);
                        }
                    }
                }
                catch (Exception)
                {
                    item.TryCount++;
                    if (item.TryCount == 3)
                    {
                        await fluentEmail
                        .To("tanersaydam@gmail.com")
                        .Subject("Send Task Is Failed")
                        .Body($"<h1><b>We cannot available the send email this task:<br>Email: {item.Todo.Email}<br>Work: {item.Todo.Work} </b></h1>")
                        .SendAsync(stoppingToken);
                    }
                }

                outBoxEmailRepository.Update(item);
                await unitOfWork.SaveChangesAsync(stoppingToken);


                await Task.Delay(1000);
            }

            await Task.Delay(10000);
        }
    }
}
