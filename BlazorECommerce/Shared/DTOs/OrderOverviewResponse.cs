namespace BlazorECommerce.Shared.DTOs;

public class OrderOverviewResponse
{
    public int OrderId { get; init; }

    public DateTime OrderDate { get; init; }

    public decimal TotalPrice { get; init; }

    public string ProductName { get; init; } = string.Empty;

    public string ProductImageUrl { get; init; } = string.Empty;
}
