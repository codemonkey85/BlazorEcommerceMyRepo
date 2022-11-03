using BlazorECommerce.Shared.Models;

namespace BlazorECommerce.Client.Services.CategoryServices;

public record CategoryService(HttpClient HttpClient) : ICategoryService
{
    public event Action? OnChange;

    public List<Category> Categories { get; set; } = new();

    public List<Category> AdminCategories { get; set; } = new();

    public async Task GetCategoriesAsync()
    {
        var results = await HttpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>($"api/{nameof(Category)}");
        if (results is { Data: not null })
        {
            Categories = results.Data;
        }
    }

    public async Task GetAdminCategoriesAsync()
    {
        var results =
            await HttpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>($"api/{nameof(Category)}/admin");
        if (results is { Data: not null })
        {
            AdminCategories = results.Data;
        }
    }

    public async Task AddCategoryAsync(Category category)
    {
        var response = await HttpClient.PostAsJsonAsync($"api/{nameof(Category)}/admin", category);
        AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>())?.Data ??
                          new List<Category>();
        await GetCategoriesAsync();
        OnChange?.Invoke();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        var response = await HttpClient.PutAsJsonAsync($"api/{nameof(Category)}/admin", category);
        AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>())?.Data ??
                          new List<Category>();
        await GetCategoriesAsync();
        OnChange?.Invoke();
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        var response = await HttpClient.DeleteAsync($"api/{nameof(Category)}/admin/{categoryId}");
        AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>())?.Data ??
                          new List<Category>();
        await GetCategoriesAsync();
        OnChange?.Invoke();
    }

    public Category CreateNewCategory()
    {
        var newCateogry = new Category { IsNew = true, IsEditing = true };
        AdminCategories.Add(newCateogry);
        OnChange?.Invoke();
        return newCateogry;
    }
}
