namespace BlazorECommerce.Client.Services.CategoryServices;

public record CategoryService(HttpClient HttpClient) : ICategoryService
{
    public List<Category> Categories { get; set; } = new();

    public async Task GetCategoriesAsync()
    {
        var results = await HttpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>($"api/{nameof(Category)}");
        if (results is { Data: not null })
        {
            Categories = results.Data;
        }
    }
}
