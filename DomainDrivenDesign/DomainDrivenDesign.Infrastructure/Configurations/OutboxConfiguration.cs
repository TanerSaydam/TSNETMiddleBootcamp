using DomainDrivenDesign.Domain.Outboxes;
using DomainDrivenDesign.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDrivenDesign.Infrastructure.Configurations;
internal sealed class OutboxConfiguration : IEntityTypeConfiguration<OutBox>
{
    public void Configure(EntityTypeBuilder<OutBox> builder)
    {
        builder.Property(p => p.Id).HasConversion(id => id.Value, value => new Identity(value));
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Subject).HasConversion(subject => subject.Value, value => new Subject(value));
        builder.OwnsOne(p => p.To, builder =>
        {
            builder.Property(p => p.Value).HasColumnName("To");
        });
        builder.Property(p => p.Body).HasConversion(body => body.Value, value => new Body(value));
        builder.Property(p => p.IsSend).HasConversion(isSend => isSend.Value, value => new IsSend(value));
        builder.OwnsOne(p => p.SendDate, builder =>
        {
            builder.Property(p => p.Value).HasColumnName("SendDate");
        });

        builder.OwnsOne(p => p.TryCount, builder =>
        {
            builder.Property(p => p.Value).HasColumnName("TryCount");
        });

        builder.HasQueryFilter(x => x.IsSend == new IsSend(false) && x.TryCount.Value < 3);
    }
}
