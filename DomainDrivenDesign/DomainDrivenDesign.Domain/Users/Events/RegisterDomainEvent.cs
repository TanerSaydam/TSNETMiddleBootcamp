using MediatR;

namespace DomainDrivenDesign.Domain.Users.Events;
public sealed record RegisterDomainEvent(string Email) : INotification;
