namespace BlazorECommerce.Rcl.Services.ProductServices;

public record ProductService(HttpClient HttpClient) : IProductService
{
    public event Action? ProductsChanged;

    public List<Product> Products { get; set; } = new();

    public async Task GetProductsAsync(string? categoryUrl = null)
    {
        var requestUrl = categoryUrl is { Length: > 0 }
            ? $"api/{nameof(Product)}/{nameof(Category)}/{categoryUrl}"
            : $"api/{nameof(Product)}";

        var result = await HttpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>(requestUrl);

        if (result is { Data: not null })
        {
            Products = result.Data;
        }

        ProductsChanged?.Invoke();
    }

    public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
    {
        var result = await HttpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/{nameof(Product)}/{productId}");
        return result!;
    }
}
