namespace BlazorECommerce.Shared.Services.CategoryServices;

public interface ICategoryService
{
    List<Category> Categories { get; set; }

    Task GetCategoriesAsync();
}
