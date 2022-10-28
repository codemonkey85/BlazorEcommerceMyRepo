namespace BlazorECommerce.Rcl.Services.ProductServices;

public record ProductService(HttpClient HttpClient) : IProductService
{
    public List<Product> Products { get; set; } = new();

    public async Task GetProductsAsync()
    {
        var result = await HttpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/{nameof(Product)}");
        if (result is { Data: not null })
        {
            Products = result.Data;
        }
    }

    public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
    {
        var result = await HttpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/{nameof(Product)}/{productId}");
        return result!;
    }
}
