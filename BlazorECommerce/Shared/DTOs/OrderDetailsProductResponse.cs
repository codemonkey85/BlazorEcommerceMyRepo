namespace BlazorECommerce.Shared.DTOs;

public class OrderDetailsProductResponse
{
    public int ProductId { get; set; }

    public string Title { get; set; }

    public int ProductTypeId { get; set; }

    public string ProductType { get; set; }

    public string ImageUrl { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }
}
