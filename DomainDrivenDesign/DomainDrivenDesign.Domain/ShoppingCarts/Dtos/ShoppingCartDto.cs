namespace DomainDrivenDesign.Domain.ShoppingCarts.Dtos;
public sealed record ShoppingCartDto(
    Guid Id,
    Guid ProductId,
    string ProductName,
    decimal Quantity,
    decimal Price
    );
