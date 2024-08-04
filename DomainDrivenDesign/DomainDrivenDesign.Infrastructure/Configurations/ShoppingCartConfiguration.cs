using DomainDrivenDesign.Domain.Products;
using DomainDrivenDesign.Domain.Shared;
using DomainDrivenDesign.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDrivenDesign.Infrastructure.Configurations;
internal sealed class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.ToTable("ShoppingCarts");
        builder.Property(p => p.Id).HasConversion(id => id.Value, value => new Identity(value));
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ProductId).HasConversion(productId => productId.Value, value => new Identity(value));

        builder.OwnsOne(p => p.Quantity, builder =>
        {
            builder.Property(p => p.Value).HasColumnName("Quantity");
        });

        builder.OwnsOne(p => p.CreatedAt, builder =>
        {
            builder.Property(p => p.Value).HasColumnName("CreatedAt");
        });

        builder
            .HasOne(p => p.Product)
            .WithMany(p => p.ShoppingCarts)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(x => x.Product!.IsDelete == new IsDelete(false));
    }
}//11:20 görüşelim