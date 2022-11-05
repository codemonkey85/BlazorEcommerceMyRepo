namespace BlazorECommerce.Client.Services;

public record ProductTypeService(HttpClient HttpClient) : IProductTypeService
{
    public event Action? OnChange;

    public List<ProductType> ProductTypes { get; set; } = new();

    public async Task GetProductTypesAsync()
    {
        var results =
            await HttpClient.GetFromJsonAsync<ServiceResponse<List<ProductType>>>($"api/{nameof(ProductType)}/admin");
        ProductTypes = results?.Data ?? new List<ProductType>();
    }

    public async Task AddProductTypeAsync(ProductType productType)
    {
        var response = await HttpClient.PostAsJsonAsync($"api/{nameof(ProductType)}/admin", productType);
        ProductTypes = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<ProductType>>>())?.Data ??
                       new List<ProductType>();
        OnChange?.Invoke();
    }

    public async Task UpdateProductTypeAsync(ProductType productType)
    {
        var response = await HttpClient.PutAsJsonAsync($"api/{nameof(ProductType)}/admin", productType);
        ProductTypes = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<ProductType>>>())?.Data ??
                       new List<ProductType>();
        OnChange?.Invoke();
    }

    public ProductType CreateNewProductType()
    {
        var newProductType = new ProductType { IsNew = true, IsEditing = true };
        ProductTypes.Add(newProductType);
        OnChange?.Invoke();
        return newProductType;
    }
}
