namespace BlazorECommerce.Shared.Services;

public interface IProductService
{
    event Action? ProductsChanged;

    List<Product> Products { get; }

    List<Product>? AdminProducts { get; set; }

    string Message { get; }

    int CurrentPage { get; }

    int PageCount { get; }

    string LastSearchText { get; }

    Task GetProductsAsync(string? categoryUrl = null);

    Task<ServiceResponse<Product>> GetProductAsync(int productId);

    Task SearchProductsAsync(string searchText, int page);

    Task<List<string>> GetProductSearchSuggestionsAsync(string searchText);

    Task GetAdminProductsAsync();

    Task<Product?> CreateProductAsync(Product product);

    Task<Product?> UpdateProductAsync(Product product);

    Task DeleteProductAsync(Product product);
}
