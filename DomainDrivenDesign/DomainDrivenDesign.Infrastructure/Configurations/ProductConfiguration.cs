using DomainDrivenDesign.Domain.Products;
using DomainDrivenDesign.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDrivenDesign.Infrastructure.Configurations;
internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> build)
    {
        build.Property(p => p.Id).HasConversion(id => id.Value, value => new Identity(value));
        build.HasKey(p => p.Id);

        build.OwnsOne(p => p.Name, builder =>
        {
            builder.Property(b => b.Value).HasColumnType("varchar(50)").HasColumnName("Name");
        });

        build.Property(p => p.Description)
        .HasConversion(description => description.Value, value => new Description(value))
        .HasColumnType("varchar(100)");

        build.OwnsOne(p => p.Price, builder =>
        {
            builder.Property(b => b.Value).HasColumnType("money").HasColumnName("Price");
        });
        build.OwnsOne(p => p.Stock, builder =>
        {
            builder.Property(b => b.Value).HasColumnType("int").HasColumnName("Stock");
        });

        build.Property(p => p.IsDelete)
            .HasConversion(isDelete => isDelete.Value, value => new IsDelete(value));

        build.HasQueryFilter(x => x.IsDelete == new IsDelete(false));
    }
}
