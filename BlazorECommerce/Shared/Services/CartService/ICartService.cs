namespace BlazorECommerce.Shared.Services.CartService;

public interface ICartService
{
    event Action? OnChange;

    Task AddToCartAsync(CartItem cartItem);

    Task<List<CartProductResponse>> GetCartProductsAsync();

    Task RemoveProductAsync(int productId, int productTypeId);

    Task UpdateQuantity(CartProductResponse product);

    Task StoreCartItemsAsync(bool emptyLocalCart);

    Task GetCartItemsCountAsync();
}
