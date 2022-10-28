namespace BlazorECommerce.Server.Endpoints;

public static class CategoryApi
{
    public static IEndpointRouteBuilder MapCategoryApi(this IEndpointRouteBuilder apiGroup)
    {
        var categoryGroup = apiGroup.MapGroup(nameof(Category));

        categoryGroup.MapGet("/", GetCategories);
        categoryGroup.MapGet("/{categoryId:int}", GetCategory);
        /*
            categoryGroup.MapPost("/", PostCategory);
            categoryGroup.MapPut("/{id:int}", PutCategory);
            categoryGroup.MapDelete("/{id:int}", DeleteCategory);
         */

        return apiGroup;
    }

    private static async Task<Ok<ServiceResponse<List<Category>>>> GetCategories(ICategoryService categoryService)
    {
        var response = await categoryService.GetCategoriesAsync();
        return TypedResults.Ok(response);
    }

    private static async Task<Ok<ServiceResponse<Category>>> GetCategory(ICategoryService categoryService,
        int categoryId)
    {
        var result = await categoryService.GetCategoryAsync(categoryId);
        return TypedResults.Ok(result);
    }
}
