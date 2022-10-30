namespace BlazorECommerce.Rcl.Pages;

public partial class Index
{
    [Parameter] public string? CategoryUrl { get; set; }

    [Parameter] public string? SearchText { get; set; }

    [Parameter] public int Page { get; set; } = 1;

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
    }
}
