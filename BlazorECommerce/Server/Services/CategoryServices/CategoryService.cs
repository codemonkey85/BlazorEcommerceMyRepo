namespace BlazorECommerce.Server.Services.CategoryServices;

public record CategoryService(DatabaseContext DatabaseContext) : ICategoryService
{
    public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
    {
        var data = await DatabaseContext.Categories.ToListAsync();
        return new ServiceResponse<List<Category>>(data);
    }

    public async Task<ServiceResponse<Category>> GetCategoryAsync(int categoryId) =>
        await DatabaseContext.Categories.FindAsync(categoryId) switch
        {
            null => new ServiceResponse<Category> { Success = false, Message = $"{nameof(Category)} not found." },
            var category => new ServiceResponse<Category>(category)
        };
}
