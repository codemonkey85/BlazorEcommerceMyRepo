namespace BlazorECommerce.Rcl.Pages;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Cart
{
    private List<CartProductResponse>? CartProducts { get; set; }

    private string Message { get; set; } = "Loading cart...";

    protected override async Task OnInitializedAsync() => await LoadCart();

    private async Task LoadCart()
    {
        await CartService.GetCartItemsCountAsync();
        CartProducts = await CartService.GetCartProductsAsync();
        if (CartProducts is not { Count: > 0 })
        {
            Message = "Your cart is empty.";
        }
    }

    private async Task RemoveProductFromCartAsync(int productId, int productTypeId)
    {
        await CartService.RemoveProductAsync(productId, productTypeId);
        await LoadCart();
    }

    private async Task UpdateQuantityAsync(CartProductResponse? cartProductResponse)
    {
        switch (cartProductResponse)
        {
            case null:
                return;
            case { Quantity: < 0 }:
                cartProductResponse.Quantity = 1;
                break;
        }

        await CartService.UpdateQuantity(cartProductResponse);
    }

    private async Task PlaceOrderAsync()
    {
        var url = await OrderService.PlaceOrderAsync();
        NavigationManager.NavigateTo(url);
    }
}
