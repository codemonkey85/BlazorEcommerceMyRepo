namespace BlazorECommerce.Server.Endpoints;

public static class CartApi
{
    public static IEndpointRouteBuilder MapCartApi(this IEndpointRouteBuilder apiGroup)
    {
        var cartGroup = apiGroup.MapGroup("cart");

        cartGroup.MapPost("products", GetCartProductsAsync);
        cartGroup.MapPost(string.Empty, StoreCartItemsAsync);
        cartGroup.MapGet("count", GetCartItemsCountAsync);
        cartGroup.MapGet(string.Empty, GetCartItemsAsync);

        return apiGroup;
    }

    private static async Task<Ok<ServiceResponse<List<CartProductResponse>>>> GetCartProductsAsync(
        ICartService cartService, List<CartItem> cartItems)
    {
        var result = await cartService.GetCartProductsAsync(cartItems);
        return TypedResults.Ok(result);
    }

    [Authorize]
    private static async
        Task<Ok<ServiceResponse<List<CartProductResponse>>>> StoreCartItemsAsync(ICartService cartService,
            List<CartItem> cartItems)
    {
        var result = await cartService.StoreCartItemsAsync(cartItems);
        return TypedResults.Ok(result);
    }

    [Authorize]
    private static async Task<ServiceResponse<int>> GetCartItemsCountAsync(ICartService cartService) =>
        await cartService.GetCartItemsCountAsync();

    [Authorize]
    private static async Task<Ok<ServiceResponse<List<CartProductResponse>>>> GetCartItemsAsync(
        ICartService cartService)
    {
        var result = await cartService.GetDbCartProductsAsync();
        return TypedResults.Ok(result);
    }
}
