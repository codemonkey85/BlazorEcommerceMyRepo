namespace BlazorECommerce.Rcl.Services.ProductServices;

public interface IProductService
{
    event Action ProductsChanged;

    List<Product> Products { get; set; }

    string Message { get; set; }

    Task GetProductsAsync(string? categoryUrl = null);

    Task<ServiceResponse<Product>> GetProductAsync(int productId);

    Task SearchProductsAsync(string searchText);

    Task<List<string>> GetProductSearchSuggestionsAsync(string searchText);
}
