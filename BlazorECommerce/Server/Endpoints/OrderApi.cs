namespace BlazorECommerce.Server.Endpoints;

public static class OrderApi
{
    public static IEndpointRouteBuilder MapOrderApi(this IEndpointRouteBuilder apiGroup)
    {
        var orderGroup = apiGroup.MapGroup(nameof(Order));

        orderGroup.MapGet(string.Empty, GetOrdersAsync);
        orderGroup.MapGet("/{orderId:int}", GetOrderDetailsAsync);

        return apiGroup;
    }

    private static async Task<Ok<ServiceResponse<List<OrderOverviewResponse>>>> GetOrdersAsync(
        IOrderService orderService)
    {
        var response = await orderService.GetOrdersAsync();
        return TypedResults.Ok(response);
    }

    private static async Task<Ok<ServiceResponse<OrderDetailsResponse>>> GetOrderDetailsAsync(
        IOrderService orderService, int orderId)
    {
        var response = await orderService.GetOrderDetailsAsync(orderId);
        return TypedResults.Ok(response);
    }
}
