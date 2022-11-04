namespace BlazorECommerce.Server.Endpoints;

public static class ProductTypeApi
{
    public static IEndpointRouteBuilder MapProductTypeApi(this IEndpointRouteBuilder apiGroup)
    {
        var productTypeGroup = apiGroup.MapGroup(nameof(ProductType));

        productTypeGroup.MapGet("admin", GetProductTypesAsync);

        return apiGroup;
    }

    [Authorize(Roles = "Admin")]
    private static async Task<Ok<ServiceResponse<List<ProductType>>>> GetProductTypesAsync(IProductTypeService productTypeService)
    {
        var response = await productTypeService.GetProductTypesAsync();
        return TypedResults.Ok(response);
    }
}
