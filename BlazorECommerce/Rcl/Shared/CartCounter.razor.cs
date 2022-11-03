namespace BlazorECommerce.Rcl.Shared;

public partial class CartCounter : IDisposable
{
    protected override void OnInitialized() => CartService.OnChange += StateHasChanged;

    public void Dispose() => CartService.OnChange -= StateHasChanged;

    private int GetCartItemsCount()
    {
        var count = SyncLocalStorageService.GetItem<int>("cartItemsCount");
        return count;
    }
}
