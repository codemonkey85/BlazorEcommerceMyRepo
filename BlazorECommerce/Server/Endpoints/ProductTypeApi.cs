namespace BlazorECommerce.Server.Endpoints;

public static class ProductTypeApi
{
    public static IEndpointRouteBuilder MapProductTypeApi(this IEndpointRouteBuilder apiGroup)
    {
        var productTypeGroup = apiGroup.MapGroup(nameof(ProductType));

        productTypeGroup.MapGet("admin", GetProductTypesAsync);
        productTypeGroup.MapPost("admin", AddProductTypeAsync);
        productTypeGroup.MapPut("admin", UpdateProductTypeAsync);

        return apiGroup;
    }

    [Authorize(Roles = "Admin")]
    private static async Task<Ok<ServiceResponse<List<ProductType>>>> GetProductTypesAsync(IProductTypeService productTypeService)
    {
        var response = await productTypeService.GetProductTypesAsync();
        return TypedResults.Ok(response);
    }

    [Authorize(Roles = "Admin")]
    private static async Task<Ok<ServiceResponse<List<ProductType>>>> AddProductTypeAsync(
        IProductTypeService productTypeService, ProductType productType)
    {
        var response = await productTypeService.AddProductTypeAsync(productType);
        return TypedResults.Ok(response);
    }

    [Authorize(Roles = "Admin")]
    private static async Task<Ok<ServiceResponse<List<ProductType>>>> UpdateProductTypeAsync(
        IProductTypeService productTypeService, ProductType productType)
    {
        var response = await productTypeService.UpdateProductTypeAsync(productType);
        return TypedResults.Ok(response);
    }
}
