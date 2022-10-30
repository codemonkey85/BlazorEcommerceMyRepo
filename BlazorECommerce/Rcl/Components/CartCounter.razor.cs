namespace BlazorECommerce.Rcl.Components;

public partial class CartCounter : IDisposable
{
    protected override void OnInitialized() => CartService.OnChange += StateHasChanged;

    public void Dispose() => CartService.OnChange -= StateHasChanged;

    private int GetCartItemsCount()
    {
        var cart = SyncLocalStorageService.GetItem<List<CartItem>>("cart") ?? new List<CartItem>();
        return cart.Count;
    }
}
