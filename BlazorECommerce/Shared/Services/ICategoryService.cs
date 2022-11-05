namespace BlazorECommerce.Shared.Services;

public interface ICategoryService
{
    event Action OnChange;

    List<Category> Categories { get; set; }

    List<Category> AdminCategories { get; set; }

    Task GetCategoriesAsync();

    Task GetAdminCategoriesAsync();

    Task AddCategoryAsync(Category category);

    Task UpdateCategoryAsync(Category category);

    Task DeleteCategoryAsync(int categoryId);

    Category CreateNewCategory();
}
