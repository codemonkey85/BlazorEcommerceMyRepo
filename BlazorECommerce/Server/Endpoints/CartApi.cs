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
        cartGroup.MapPost("add", AddToCartAsync);
        cartGroup.MapPut("updatequantity", UpdateQuantityAsync);
        cartGroup.MapDelete("{productId:int}/{productTypeId:int}", RemoveItemFromCartAsync);

        return apiGroup;
    }

    private static async Task<Ok<ServiceResponse<List<CartProductResponse>>>> GetCartProductsAsync(
        ICartService cartService, List<CartItem> cartItems)
    {
        var results = await cartService.GetCartProductsAsync(cartItems);
        return TypedResults.Ok(results);
    }

    private static async
        Task<Ok<ServiceResponse<List<CartProductResponse>>>> StoreCartItemsAsync(ICartService cartService,
            List<CartItem> cartItems)
    {
        var results = await cartService.StoreCartItemsAsync(cartItems);
        return TypedResults.Ok(results);
    }

    private static async Task<ServiceResponse<int>> GetCartItemsCountAsync(ICartService cartService) =>
        await cartService.GetCartItemsCountAsync();

    private static async Task<Ok<ServiceResponse<List<CartProductResponse>>>> GetCartItemsAsync(
        ICartService cartService)
    {
        var results = await cartService.GetDbCartProductsAsync();
        return TypedResults.Ok(results);
    }

    private static async Task<Ok<ServiceResponse<bool>>> AddToCartAsync(ICartService cartService, CartItem cartItem)
    {
        var results = await cartService.AddToCartAsync(cartItem);
        return TypedResults.Ok(results);
    }

    private static async Task<Ok<ServiceResponse<bool>>> UpdateQuantityAsync(ICartService cartService,
        CartItem cartItem)
    {
        var results = await cartService.UpdateQuantityAsync(cartItem);
        return TypedResults.Ok(results);
    }

    private static async Task<Ok<ServiceResponse<bool>>> RemoveItemFromCartAsync(ICartService cartService,
        int productId, int productTypeId)
    {
        var results = await cartService.RemoveItemFromCartAsync(productId, productTypeId);
        return TypedResults.Ok(results);
    }
}
