namespace BlazorECommerce.Rcl.Services;

public record ProductService(HttpClient HttpClient) : IProductService
{
    public List<Product> Products { get; set; } = new List<Product>();

    public async Task GetProductsAsync()
    {
        var result = await HttpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/{nameof(Product)}");
        if (result is { Data: not null })
        {
            Products = result.Data;
        }
    }
}
