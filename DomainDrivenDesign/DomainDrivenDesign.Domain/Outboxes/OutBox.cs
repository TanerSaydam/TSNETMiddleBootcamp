using DomainDrivenDesign.Domain.Shared;

namespace DomainDrivenDesign.Domain.Outboxes;
public sealed class OutBox
{
    private OutBox()
    {

    }
    public OutBox(To to, Subject subject, Body body)
    {
        Id = Identity.Create();
        Subject = subject;
        Body = body;
        To = to;
        IsSend = new(false);
        TryCount = new(0);
    }

    public Identity Id { get; private set; } = default!;
    public Subject Subject { get; private set; } = default!;
    public Body Body { get; private set; } = default!;
    public To To { get; private set; } = default!;
    public IsSend IsSend { get; private set; } = default!;
    public SendDate? SendDate { get; private set; } = null;
    public TryCount TryCount { get; private set; } = default!;

    public void SetTryCount()
    {
        TryCount = new(TryCount.Value + 1);
    }

    public void SetIsSend()
    {
        IsSend = new(true);
        SendDate = new(DateTime.Now);
        TryCount = new(0);
    }
}