namespace BlazorECommerce.Shared.DTOs;

public class ProductSearchResult
{
    public List<Product> Products { get; init; } = new();

    public int Pages { get; init; }

    public int CurrentPage { get; init; }
}
