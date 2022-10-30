namespace BlazorECommerce.Rcl.Pages;

public partial class Cart
{
    private List<CartProductResponse>? CartProducts { get; set; }

    private string Message { get; set; } = "Loading cart...";

    protected override async Task OnInitializedAsync() => await LoadCart();

    private async Task LoadCart()
    {
        if (await CartService.ItemCount() <= 0)
        {
            Message = "Your cart is empty.";
            CartProducts = new List<CartProductResponse>();
        }
        else
        {
            CartProducts = await CartService.GetCartProductsAsync();
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
}
