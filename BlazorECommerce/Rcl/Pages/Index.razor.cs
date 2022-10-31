namespace BlazorECommerce.Rcl.Pages;

public partial class Index
{
    [Parameter] public string? CategoryUrl { get; set; }

    [Parameter] public string? SearchText { get; set; }

    [Parameter] public int Page { get; set; } = 1;

    private const string DefaultPageTitle = "My Shop";
    private string pageTitle = DefaultPageTitle;

    protected override async Task OnParametersSetAsync()
    {
        if (SearchText is { Length: > 0 })
        {
            await ProductService.SearchProductsAsync(SearchText, Page);
        }
        else
        {
            await ProductService.GetProductsAsync(CategoryUrl);
        }

        if (CategoryUrl is { Length: > 0 })
        {
            var categoryTitle = CategoryService.Categories
                .Where(category => category.Url == CategoryUrl)
                .Select(category => category.Name)
                .FirstOrDefault();
            pageTitle = categoryTitle ?? DefaultPageTitle;
        }
    }
}
