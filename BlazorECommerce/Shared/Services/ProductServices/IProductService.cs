namespace BlazorECommerce.Shared.Services.ProductServices;

public interface IProductService
{
    event Action? ProductsChanged;

    List<Product> Products { get; }

    string Message { get; }

    int CurrentPage { get; }

    int PageCount { get; }

    string LastSearchText { get; }

    Task GetProductsAsync(string? categoryUrl = null);

    Task<ServiceResponse<Product>> GetProductAsync(int productId);

    Task SearchProductsAsync(string searchText, int page);

    Task<List<string>> GetProductSearchSuggestionsAsync(string searchText);
}
