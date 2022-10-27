namespace BlazorECommerce.Server.Endpoints;

public static class ProductApi
{
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

    private static async Task<Ok<ServiceResponse<List<Product>>>> GetProducts(DatabaseContext databaseContext)
    {
        var products = await databaseContext.Products.ToListAsync();
        var response = new ServiceResponse<List<Product>>()
        {
            Data = products
        };
        return TypedResults.Ok(response);
    }
}
