namespace BlazorECommerce.Rcl.Pages;

public partial class Cart
{
    private List<CartProductResponse>? CartProducts { get; set; }

    private string Message { get; set; } = "Loading cart...";

    private bool orderPlaced = false;

    protected override async Task OnInitializedAsync()
    {
        await LoadCart();
        orderPlaced = false;
    }

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
        await OrderService.PlaceOrderAsync();
        CartProducts = await CartService.GetCartProductsAsync();
        await CartService.GetCartItemsCountAsync();
        orderPlaced = true;
    }
}
