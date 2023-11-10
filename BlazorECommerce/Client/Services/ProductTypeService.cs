namespace BlazorECommerce.Client.Services;

public record ProductTypeService(HttpClient HttpClient) : IProductTypeService
{
    public event Action? OnChange;

    public List<ProductType> ProductTypes { get; set; } = [];

    public async Task GetProductTypesAsync()
    {
        var results =
            await HttpClient.GetFromJsonAsync<ServiceResponse<List<ProductType>>>(Constants.AdminProductTypeApi);
        ProductTypes = results?.Data ?? [];
    }

    public async Task AddProductTypeAsync(ProductType productType)
    {
        var response = await HttpClient.PostAsJsonAsync(Constants.AdminProductTypeApi, productType);
        ProductTypes = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<ProductType>>>())?.Data ?? [];
        OnChange?.Invoke();
    }

    public async Task UpdateProductTypeAsync(ProductType productType)
    {
        var response = await HttpClient.PutAsJsonAsync(Constants.AdminProductTypeApi, productType);
        ProductTypes = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<ProductType>>>())?.Data ?? [];
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
