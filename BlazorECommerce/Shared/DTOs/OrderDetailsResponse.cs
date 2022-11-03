namespace BlazorECommerce.Shared.DTOs;

public class OrderDetailsResponse
{
    public DateTime OrderDate { get; init; }

    public decimal TotalPrice { get; init; }

    public List<OrderDetailsProductResponse> Products { get; init; } = new();
}
