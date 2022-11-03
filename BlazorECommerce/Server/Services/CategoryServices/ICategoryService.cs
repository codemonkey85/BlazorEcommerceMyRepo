namespace BlazorECommerce.Server.Services.CategoryServices;

public interface ICategoryService
{
    Task<ServiceResponse<List<Category>>> GetCategoriesAsync();
}
