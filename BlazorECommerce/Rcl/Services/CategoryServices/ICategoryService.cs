namespace BlazorECommerce.Rcl.Services.CategoryServices;

public interface ICategoryService
{
    List<Category> Categories { get; set; }

    Task GetCategoriesAsync();

    Task<ServiceResponse<Category>> GetCategoryAsync(int categoryId);
}
