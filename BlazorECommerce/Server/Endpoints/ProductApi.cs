namespace BlazorECommerce.Server.Endpoints;

public static class ProductApi
{
    public static IEndpointRouteBuilder MapProductApi(this IEndpointRouteBuilder apiGroup)
    {
        var productGroup = apiGroup.MapGroup(nameof(Product));

        productGroup.MapGet("/", GetProducts);
        productGroup.MapGet("/{productId:int}", GetProduct);
        /*
            productGroup.MapPost("/", PostProduct);
            productGroup.MapPut("/{id:int}", PutProduct);
            productGroup.MapDelete("/{id:int}", DeleteProduct);
         */

        return apiGroup;
    }

    private static async Task<Ok<ServiceResponse<List<Product>>>> GetProducts(IProductService productService)
    {
        var response = await productService.GetProductsAsync();
        return TypedResults.Ok(response);
    }

    private static async Task<Ok<ServiceResponse<Product>>> GetProduct(IProductService productService, int productId)
    {
        var result = await productService.GetProductAsync(productId);
        return TypedResults.Ok(result);
    }
}
