namespace BlazorECommerce.Rcl.Pages.Admin;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Categories : IDisposable
{
    private Category? editingCategory;

    protected override async Task OnInitializedAsync()
    {
        await CategoryService.GetAdminCategoriesAsync();
        CategoryService.OnChange += StateHasChanged;
    }

    public void Dispose() => CategoryService.OnChange -= StateHasChanged;

    private void CreateNewCategory() => editingCategory = CategoryService.CreateNewCategory();

    private void EditCategory(Category category)
    {
        category.IsEditing = true;
        editingCategory = category;
    }

    private async Task UpdateCategoryAsync()
    {
        var categoryTask = editingCategory switch
        {
            null => null,
            { IsNew: true } => CategoryService.AddCategoryAsync(editingCategory),
            { IsNew: false } => CategoryService.UpdateCategoryAsync(editingCategory)
        };

        if (categoryTask is not null)
        {
            await categoryTask;
        }

        editingCategory = new Category();
    }

    private async Task DeleteCategoryAsync(int categoryId) =>
        await CategoryService.DeleteCategoryAsync(categoryId);

    private async Task CancelEditingAsync()
    {
        editingCategory = new Category();
        await CategoryService.GetAdminCategoriesAsync();
    }
}
