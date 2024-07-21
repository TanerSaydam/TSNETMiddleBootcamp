using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Infrastructure.Context;
internal sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}