namespace BlazorECommerce.Rcl.Components;

public partial class Search
{
    private string searchText = string.Empty;
    private List<string> suggestions = new();
    private ElementReference searchInput;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        await searchInput.FocusAsync();
    }

    private void SearchProducts() => NavigationManager.NavigateTo($"search/{searchText}/1");

    private async Task HandleSearch(KeyboardEventArgs args)
    {
        if (args.Key is null or "Enter")
        {
            SearchProducts();
        }
        else if (searchText is { Length: > 1 })
        {
            suggestions = await ProductService.GetProductSearchSuggestionsAsync(searchText);
        }
    }
}
