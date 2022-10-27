namespace BlazorECommerce.Shared.Models;

public class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,4)")]
    public decimal Price { get; set; }
}
