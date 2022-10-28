namespace BlazorECommerce.Server.Endpoints;

public static class ProductApi
{
    public static IEndpointRouteBuilder MapProductApi(this IEndpointRouteBuilder apiGroup)
    {
        var productGroup = apiGroup.MapGroup(nameof(Product));

        productGroup.MapGet("/", GetProducts);
        productGroup.MapGet("/{productId:int}", GetProduct);
        productGroup.MapGet($"/{nameof(Category)}/{{categoryUrl}}", GetProductsByCategory);
        productGroup.MapGet("/search/{searchText}", SearchProducts);
        productGroup.MapGet("/searchsuggestions/{searchText}", GetProductSearchSuggestions);
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

    private static async Task<Ok<ServiceResponse<List<Product>>>> GetProductsByCategory(IProductService productService,
        string categoryUrl)
    {
        var result = await productService.GetProductsByCategoryAsync(categoryUrl);
        return TypedResults.Ok(result);
    }

    private static async Task<Ok<ServiceResponse<List<Product>>>> SearchProducts(IProductService productService,
        string searchText)
    {
        var response = await productService.SearchProductsAsync(searchText);
        return TypedResults.Ok(response);
    }

    private static async Task<Ok<ServiceResponse<List<string>>>> GetProductSearchSuggestions(
        IProductService productService,
        string searchText)
    {
        var response = await productService.GetProductSearchSuggestions(searchText);
        return TypedResults.Ok(response);
    }
}
