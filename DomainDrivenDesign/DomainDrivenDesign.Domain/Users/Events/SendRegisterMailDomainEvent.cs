using DomainDrivenDesign.Domain.Outboxes;
using DomainDrivenDesign.Domain.Services;
using MediatR;

namespace DomainDrivenDesign.Domain.Users.Events;

public sealed class SendRegisterMailDomainEvent(
    IOutboxRepository outboxRepository) : INotificationHandler<RegisterDomainEvent>
{
    public async Task Handle(RegisterDomainEvent notification, CancellationToken cancellationToken)
    {
        Subject subject = new("Register");
        Body body = new(EmailTemplateService.CreateRegisterBody(notification.Email!));
        To to = new(notification.Email!);
        OutBox outBox = new(to, subject, body);

        await outboxRepository.CreateAsync(outBox, cancellationToken);
    }
}
