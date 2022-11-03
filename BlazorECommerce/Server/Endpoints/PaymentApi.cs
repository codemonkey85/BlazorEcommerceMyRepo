namespace BlazorECommerce.Server.Endpoints;

public static class PaymentApi
{
    public static IEndpointRouteBuilder MapPaymentApi(this IEndpointRouteBuilder apiGroup)
    {
        var paymentGroup = apiGroup.MapGroup("payment");

        paymentGroup.MapPost("checkout", CreateCheckoutSessionAsync);
        paymentGroup.MapPost(string.Empty, FullfillOrderAsync);

        return apiGroup;
    }

    [Authorize]
    private static async Task<Ok<string>> CreateCheckoutSessionAsync(IPaymentService paymentService)
    {
        var session = await paymentService.CreateCheckoutSession();
        return TypedResults.Ok(session.Url);
    }

    private static async Task<Results<Ok<ServiceResponse<bool>>, BadRequest<string>>> FullfillOrderAsync(
        IPaymentService paymentService,
        HttpRequest request)
    {
        var response = await paymentService.FullfillOrderAsync(request);
        return !response.Success
            ? TypedResults.BadRequest(response.Message)
            : TypedResults.Ok(response);
    }
}
