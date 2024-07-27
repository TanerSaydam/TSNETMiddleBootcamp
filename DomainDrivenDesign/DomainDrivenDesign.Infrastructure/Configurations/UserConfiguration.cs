using DomainDrivenDesign.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDrivenDesign.Infrastructure.Configurations;
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName)
            .HasConversion(firstName => firstName.Value, value => new FirstName(value));
        builder.Property(p => p.LastName)
            .HasConversion(lastName => lastName.Value, value => new LastName(value));
        builder.Property(p => p.IsAdmin)
           .HasConversion(isAdmin => isAdmin.Value, value => new IsAdmin(value));
    }
}