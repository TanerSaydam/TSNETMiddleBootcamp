using MediatR;
using TodoCleanArchitecture.Domain.Entities;

namespace TodoCleanArchitecture.Domain.Events;
public sealed class TodoDomainEvent : INotification
{
    public Todo Todo { get; set; }
    public TodoDomainEvent(Todo todo)
    {
        Todo = todo;
    }
}


public sealed class TodoSendEmailDomainEvent : INotificationHandler<TodoDomainEvent>
{
    public async Task Handle(TodoDomainEvent notification, CancellationToken cancellationToken)
    {
        //Send email
        await Task.CompletedTask;
    }
}

public sealed class TodoSendSmsDomainEvent : INotificationHandler<TodoDomainEvent>
{
    public async Task Handle(TodoDomainEvent notification, CancellationToken cancellationToken)
    {
        //Send sms
        await Task.CompletedTask;
    }
}


public sealed class TodoSendWhatsAppDomainEvent : INotificationHandler<TodoDomainEvent>
{
    public async Task Handle(TodoDomainEvent notification, CancellationToken cancellationToken)
    {
        //Send Whatsapp
        await Task.CompletedTask;
    }
}


public sealed class TodoDoSomethingDomainEvent : INotificationHandler<TodoDomainEvent>
{
    public async Task Handle(TodoDomainEvent notification, CancellationToken cancellationToken)
    {
        //Do Something
        await Task.CompletedTask;
    }
}

