namespace BlazorECommerce.Server.Endpoints;

public static class ProductApi
{
    private static readonly List<Product> Products = new();

    public static IEndpointRouteBuilder MapProductApi(this IEndpointRouteBuilder apiGroup)
    {
        var productGroup = apiGroup.MapGroup(nameof(Product));

        productGroup.MapGet("/", GetProducts);

        /*
            productGroup.MapGet("/", GetProducts);
            productGroup.MapGet("/{id:guid}", GetEntity);
            productGroup.MapPost("/", PostEntity);
            productGroup.MapPut("/{id:guid}", PutEntity);
            productGroup.MapDelete("/{id:guid}", DeleteEntity);
         */

        return apiGroup;
    }

    private static Task<Ok<List<Product>>> GetProducts() => Task.FromResult(TypedResults.Ok(Products));
}
