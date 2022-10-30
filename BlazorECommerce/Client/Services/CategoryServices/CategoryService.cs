namespace BlazorECommerce.Client.Services.CategoryServices;

public record CategoryService(HttpClient HttpClient) : ICategoryService
{
    public List<Category> Categories { get; set; } = new();

    public async Task GetCategoriesAsync()
    {
        var result = await HttpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>($"api/{nameof(Category)}");
        if (result is { Data: not null })
        {
            Categories = result.Data;
        }
    }

    public async Task<ServiceResponse<Category>> GetCategoryAsync(int categoryId)
    {
        var result =
            await HttpClient.GetFromJsonAsync<ServiceResponse<Category>>($"api/{nameof(Category)}/{categoryId}");
        return result!;
    }
}
