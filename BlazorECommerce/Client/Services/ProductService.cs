namespace BlazorECommerce.Client.Services;

public record ProductService(HttpClient HttpClient) : IProductService
{
    public event Action? ProductsChanged;

    public List<Product> Products { get; set; } = new();

    public List<Product>? AdminProducts { get; set; }

    public string Message { get; set; } = "Loading Products...";

    public int CurrentPage { get; set; } = 1;

    public int PageCount { get; set; }

    public string LastSearchText { get; set; } = string.Empty;

    public async Task GetProductsAsync(string? categoryUrl = null)
    {
        var requestUrl = categoryUrl is { Length: > 0 }
            ? $"{Constants.ProductApi}/{Constants.Category}/{categoryUrl}"
            : $"{Constants.ProductApi}/featured";

        var results = await HttpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>(requestUrl);

        if (results is { Data: not null })
        {
            Products = results.Data;
        }

        CurrentPage = 1;
        PageCount = 0;

        if (Products.Count == 0)
        {
            Message = "No products found.";
        }

        ProductsChanged?.Invoke();
    }

    public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
    {
        var results = await HttpClient.GetFromJsonAsync<ServiceResponse<Product>>($"{Constants.ProductApi}/{productId}");
        return results!;
    }

    public async Task SearchProductsAsync(string searchText, int page)
    {
        LastSearchText = searchText;

        var results =
            await HttpClient.GetFromJsonAsync<ServiceResponse<ProductSearchResult>>(
                $"{Constants.ProductApi}/search/{searchText}/{page}");

        if (results is { Data: not null })
        {
            Products = results.Data.Products;
            CurrentPage = results.Data.CurrentPage;
            PageCount = results.Data.Pages;
        }

        if (Products is { Count: 0 })
        {
            Message = "No products found.";
        }

        ProductsChanged?.Invoke();
    }

    public async Task<List<string>> GetProductSearchSuggestionsAsync(string searchText)
    {
        var results =
            await HttpClient.GetFromJsonAsync<ServiceResponse<List<string>>>(
                $"{Constants.ProductApi}/searchsuggestions/{searchText}");

        return results?.Data ?? new List<string>();
    }

    public async Task GetAdminProductsAsync()
    {
        var results = await HttpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>(Constants.AdminProductApi);
        AdminProducts = results?.Data ?? new List<Product>();
        CurrentPage = 1;
        PageCount = 0;
        if (AdminProducts is { Count: 0 })
        {
            Message = "No products found.";
        }
    }

    public async Task<Product?> CreateProductAsync(Product product)
    {
        var results = await HttpClient.PostAsJsonAsync(Constants.AdminProductApi, product);
        var newProduct = (await results.Content.ReadFromJsonAsync<ServiceResponse<Product?>>())?.Data;
        return newProduct;
    }

    public async Task<Product?> UpdateProductAsync(Product product)
    {
        var results = await HttpClient.PutAsJsonAsync(Constants.AdminProductApi, product);
        var updatedProduct = (await results.Content.ReadFromJsonAsync<ServiceResponse<Product?>>())?.Data;
        return updatedProduct;
    }

    public async Task DeleteProductAsync(Product product) =>
        await HttpClient.DeleteAsync($"{Constants.AdminProductApi}/{product.Id}");
}
