namespace BlazorECommerce.Rcl.Components;

public partial class Search
{
    private string _searchText = string.Empty;
    private List<string> _suggestions = new();
    protected ElementReference SearchInput;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        await SearchInput.FocusAsync();
    }

    public void SearchProducts() => NavigationManager.NavigateTo($"search/{_searchText}/1");

    public async Task HandleSearch(KeyboardEventArgs args)
    {
        if (args.Key is null or "Enter")
        {
            SearchProducts();
        }
        else if (_searchText is { Length: > 1 })
        {
            _suggestions = await ProductService.GetProductSearchSuggestionsAsync(_searchText);
        }
    }
}
