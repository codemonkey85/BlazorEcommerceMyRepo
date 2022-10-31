namespace BlazorECommerce.Shared.DTOs;

public class CartItem
{
    public int ProductId { get; set; }

    public int ProductTypeId { get; set; }

    public int Quantity { get; set; } = 1;
}
