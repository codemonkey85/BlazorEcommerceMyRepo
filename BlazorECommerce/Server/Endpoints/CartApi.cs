namespace BlazorECommerce.Server.Endpoints;

public static class CartApi
{
    public static IEndpointRouteBuilder MapCartApi(this IEndpointRouteBuilder apiGroup)
    {
        var cartGroup = apiGroup.MapGroup("cart");

        cartGroup.MapPost("products", GetCartProductsAsync);

        return apiGroup;
    }

    private static async Task<Ok<ServiceResponse<List<CartProductResponse>>>> GetCartProductsAsync(
        ICartService cartService, List<CartItem> cartItems)
    {
        var result = await cartService.GetCartProductsAsync(cartItems);
        return TypedResults.Ok(result);
    }
}
