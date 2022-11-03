namespace BlazorECommerce.Server.Services.CategoryServices;

public record CategoryService(DatabaseContext DatabaseContext) : ICategoryService
{
    public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
    {
        var data = await DatabaseContext.Categories.ToListAsync();
        return new ServiceResponse<List<Category>>(data);
    }
}
