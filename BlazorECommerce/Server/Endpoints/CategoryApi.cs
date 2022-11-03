namespace BlazorECommerce.Server.Endpoints;

public static class CategoryApi
{
    public static IEndpointRouteBuilder MapCategoryApi(this IEndpointRouteBuilder apiGroup)
    {
        var categoryGroup = apiGroup.MapGroup(nameof(Category));

        categoryGroup.MapGet("/", GetCategoriesAsync);
        categoryGroup.MapGet("admin", GetAdminCategoriesAsync);
        categoryGroup.MapPost("admin", AddCategoryAsync);
        categoryGroup.MapPut("admin", UpdateCategoryAsync);
        categoryGroup.MapDelete("admin/{categoryId:int}", DeleteCategoryAsync);

        return apiGroup;
    }

    private static async Task<Ok<ServiceResponse<List<Category>>>> GetCategoriesAsync(ICategoryService categoryService)
    {
        var response = await categoryService.GetCategoriesAsync();
        return TypedResults.Ok(response);
    }

    [Authorize(Roles = "Admin")]
    private static async Task<Ok<ServiceResponse<List<Category>>>> GetAdminCategoriesAsync(
        ICategoryService categoryService)
    {
        var response = await categoryService.GetAdminCategoriesAsync();
        return TypedResults.Ok(response);
    }

    [Authorize(Roles = "Admin")]
    private static async Task<Ok<ServiceResponse<List<Category>>>> AddCategoryAsync(ICategoryService categoryService,
        Category category)
    {
        var response = await categoryService.AddCategoryAsync(category);
        return TypedResults.Ok(response);
    }

    [Authorize(Roles = "Admin")]
    private static async Task<Ok<ServiceResponse<List<Category>>>> UpdateCategoryAsync(ICategoryService categoryService,
        Category category)
    {
        var response = await categoryService.UpdateCategoryAsync(category);
        return TypedResults.Ok(response);
    }

    [Authorize(Roles = "Admin")]
    private static async Task<Ok<ServiceResponse<List<Category>>>> DeleteCategoryAsync(ICategoryService categoryService,
        int categoryId)
    {
        var response = await categoryService.DeleteCategoryAsync(categoryId);
        return TypedResults.Ok(response);
    }
}
