namespace BlazorECommerce.Server.Endpoints;

public static class ProductApi
{
    public static IEndpointRouteBuilder MapProductApi(this IEndpointRouteBuilder apiGroup)
    {
        var productGroup = apiGroup.MapGroup(Constants.Product);

        productGroup.MapGet(string.Empty, GetProductsAsync);
        productGroup.MapGet("/{productId:int}", GetProductAsync);
        productGroup.MapGet($"/{nameof(Category)}/{{categoryUrl}}", GetProductsByCategoryAsync);
        productGroup.MapGet("/search/{searchText}/{page:int}", SearchProductsAsync);
        productGroup.MapGet("/searchsuggestions/{searchText}", GetProductSearchSuggestionsAsync);
        productGroup.MapGet("/featured", GetFeaturedProductsAsync);

        var adminProductGroup = productGroup.MapGroup(Constants.Admin);

        adminProductGroup.MapGet(string.Empty, GetAdminProductsAsync);
        adminProductGroup.MapPost(string.Empty, CreateProductAsync);
        adminProductGroup.MapPut(string.Empty, UpdateProductAsync);
        adminProductGroup.MapDelete("/{productId:int}", DeleteProductAsync);

        return apiGroup;
    }

    private static async Task<Ok<ServiceResponse<List<Product>>>> GetProductsAsync(IProductService productService)
    {
        var response = await productService.GetProductsAsync();
        return TypedResults.Ok(response);
    }

    private static async Task<Ok<ServiceResponse<Product>>> GetProductAsync(IProductService productService,
        IAuthService authService, int productId)
    {
        var results = await productService.GetProductAsync(authService, productId);
        return TypedResults.Ok(results);
    }

    private static async Task<Ok<ServiceResponse<List<Product>>>> GetProductsByCategoryAsync(
        IProductService productService,
        string categoryUrl)
    {
        var results = await productService.GetProductsByCategoryAsync(categoryUrl);
        return TypedResults.Ok(results);
    }

    private static async Task<Ok<ServiceResponse<ProductSearchResult>>> SearchProductsAsync(
        IProductService productService,
        string searchText, int page)
    {
        var response = await productService.SearchProductsAsync(searchText, page);
        return TypedResults.Ok(response);
    }

    private static async Task<Ok<ServiceResponse<List<string>>>> GetProductSearchSuggestionsAsync(
        IProductService productService,
        string searchText)
    {
        var response = await productService.GetProductSearchSuggestions(searchText);
        return TypedResults.Ok(response);
    }

    private static async Task<Ok<ServiceResponse<List<Product>>>> GetFeaturedProductsAsync(
        IProductService productService)
    {
        var response = await productService.GetFeaturedProductsAsync();
        return TypedResults.Ok(response);
    }

    [Authorize(Roles = "Admin")]
    private static async Task<Ok<ServiceResponse<List<Product>>>> GetAdminProductsAsync(IProductService productService)
    {
        var response = await productService.GetAdminProductsAsync();
        return TypedResults.Ok(response);
    }

    [Authorize(Roles = "Admin")]
    private static async Task<Ok<ServiceResponse<Product>>> CreateProductAsync(IProductService productService,
        Product product)
    {
        var response = await productService.CreateProductAsync(product);
        return TypedResults.Ok(response);
    }

    [Authorize(Roles = "Admin")]
    private static async Task<Ok<ServiceResponse<Product>>> UpdateProductAsync(IProductService productService,
        Product product)
    {
        var response = await productService.UpdateProductAsync(product);
        return TypedResults.Ok(response);
    }

    [Authorize(Roles = "Admin")]
    private static async Task<Ok<ServiceResponse<bool>>> DeleteProductAsync(IProductService productService,
        int productId)
    {
        var response = await productService.DeleteProductAsync(productId);
        return TypedResults.Ok(response);
    }
}
