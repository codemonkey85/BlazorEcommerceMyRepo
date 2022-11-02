namespace BlazorECommerce.Server.Endpoints;

public static class CategoryApi
{
    public static IEndpointRouteBuilder MapCategoryApi(this IEndpointRouteBuilder apiGroup)
    {
        var categoryGroup = apiGroup.MapGroup(nameof(Category));

        categoryGroup.MapGet("/", GetCategoriesAsync);
        categoryGroup.MapGet("/{categoryId:int}", GetCategoryAsync);

        return apiGroup;
    }

    private static async Task<Ok<ServiceResponse<List<Category>>>> GetCategoriesAsync(ICategoryService categoryService)
    {
        var response = await categoryService.GetCategoriesAsync();
        return TypedResults.Ok(response);
    }

    private static async Task<Ok<ServiceResponse<Category>>> GetCategoryAsync(ICategoryService categoryService,
        int categoryId)
    {
        var results = await categoryService.GetCategoryAsync(categoryId);
        return TypedResults.Ok(results);
    }
}
