namespace BlazorECommerce.Server.Services.CartServices;

public interface ICartService
{
    Task<ServiceResponse<List<CartProductResponse>>> GetCartProductsAsync(List<CartItem> cartItems);

    Task<ServiceResponse<List<CartProductResponse>>> StoreCartItemsAsync(List<CartItem> cartItems);

    Task<ServiceResponse<int>> GetCartItemsCountAsync();

    Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProductsAsync();
}
