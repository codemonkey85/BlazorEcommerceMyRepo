namespace BlazorECommerce.Shared.DTOs;

public class OrderDetailsProductResponse
{
    public int ProductId { get; init; }

    public string Title { get; init; } = string.Empty;

    public int ProductTypeId { get; init; }

    public string ProductType { get; init; } = string.Empty;

    public string ImageUrl { get; init; } = string.Empty;

    public int Quantity { get; init; }

    public decimal TotalPrice { get; init; }
}
