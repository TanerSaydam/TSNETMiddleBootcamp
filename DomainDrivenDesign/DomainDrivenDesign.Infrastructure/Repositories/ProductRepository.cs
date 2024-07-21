using DomainDrivenDesign.Domain.Products;
using DomainDrivenDesign.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Infrastructure.Repositories;
internal sealed class ProductRepository(
    ApplicationDbContext context) : IProductRepository
{
    public async Task CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await context.AddAsync(product, cancellationToken);
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Products.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<bool> IsNameExists(string name, CancellationToken cancellationToken = default)
    {
        return await context.Products.AnyAsync(p => p.Name.Value == name, cancellationToken);
    }
}
