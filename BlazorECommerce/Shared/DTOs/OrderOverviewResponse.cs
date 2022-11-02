namespace BlazorECommerce.Shared.DTOs;

public class OrderOverviewResponse
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalPrice { get; set; }

    public string ProductName { get; set; }

    public string ProductImageUrl { get; set; }
}
