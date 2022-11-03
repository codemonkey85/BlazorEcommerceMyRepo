namespace BlazorECommerce.Server.Services.CategoryServices;

public record CategoryService(DatabaseContext DatabaseContext) : ICategoryService
{
    public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
    {
        var data = await DatabaseContext.Categories
            .Where(category => category.IsVisible && !category.IsDeleted)
            .ToListAsync();
        return new ServiceResponse<List<Category>>(data);
    }

    public async Task<ServiceResponse<List<Category>>> GetAdminCategoriesAsync()
    {
        var data = await DatabaseContext.Categories
            .Where(category => !category.IsDeleted)
            .ToListAsync();
        return new ServiceResponse<List<Category>>(data);
    }

    public async Task<ServiceResponse<List<Category>>> AddCategoryAsync(Category category)
    {
        category.IsEditing = category.IsNew = false;
        DatabaseContext.Categories.Add(category);
        await DatabaseContext.SaveChangesAsync();
        return await GetAdminCategoriesAsync();
    }

    public async Task<ServiceResponse<List<Category>>> UpdateCategoryAsync(Category category)
    {
        var dbCategory = await GetCategoryByIdAsync(category.Id);
        if (dbCategory is null)
        {
            return new ServiceResponse<List<Category>> { Success = false, Message = "Category not found." };
        }

        dbCategory.Name = category.Name;
        dbCategory.Url = category.Url;
        dbCategory.IsVisible = category.IsVisible;

        await DatabaseContext.SaveChangesAsync();
        return await GetAdminCategoriesAsync();
    }

    public async Task<ServiceResponse<List<Category>>> DeleteCategoryAsync(int categoryId)
    {
        var category = await GetCategoryByIdAsync(categoryId);
        if (category is null)
        {
            return new ServiceResponse<List<Category>> { Success = false, Message = "Category not found." };
        }

        category.IsDeleted = true;

        await DatabaseContext.SaveChangesAsync();
        return await GetAdminCategoriesAsync();
    }

    private async Task<Category?> GetCategoryByIdAsync(int categoryId) =>
        await DatabaseContext.Categories.FindAsync(categoryId);
}
