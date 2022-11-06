namespace BlazorECommerce.Server.Endpoints;

public static class CategoryApi
{
    public static IEndpointRouteBuilder MapCategoryApi(this IEndpointRouteBuilder apiGroup)
    {
        var categoryGroup = apiGroup.MapGroup(Constants.Category);

        categoryGroup.MapGet("/", GetCategoriesAsync);

        var adminCategoryGroup = categoryGroup.MapGroup(Constants.Admin);

        adminCategoryGroup.MapGet(string.Empty, GetAdminCategoriesAsync);
        adminCategoryGroup.MapPost(string.Empty, AddCategoryAsync);
        adminCategoryGroup.MapPut(string.Empty, UpdateCategoryAsync);
        adminCategoryGroup.MapDelete("/{categoryId:int}", DeleteCategoryAsync);

        return apiGroup;
    }

    private static async Task<Ok<ServiceResponse<List<Category>>>> GetCategoriesAsync(ICategoryService categoryService)
    {
        var response = await categoryService.GetCategoriesAsync();
        return TypedResults.Ok(response);
    }

    [Authorize(Roles = Constants.Admin)]
    private static async Task<Ok<ServiceResponse<List<Category>>>> GetAdminCategoriesAsync(
        ICategoryService categoryService)
    {
        var response = await categoryService.GetAdminCategoriesAsync();
        return TypedResults.Ok(response);
    }

    [Authorize(Roles = Constants.Admin)]
    private static async Task<Ok<ServiceResponse<List<Category>>>> AddCategoryAsync(ICategoryService categoryService,
        Category category)
    {
        var response = await categoryService.AddCategoryAsync(category);
        return TypedResults.Ok(response);
    }

    [Authorize(Roles = Constants.Admin)]
    private static async Task<Ok<ServiceResponse<List<Category>>>> UpdateCategoryAsync(ICategoryService categoryService,
        Category category)
    {
        var response = await categoryService.UpdateCategoryAsync(category);
        return TypedResults.Ok(response);
    }

    [Authorize(Roles = Constants.Admin)]
    private static async Task<Ok<ServiceResponse<List<Category>>>> DeleteCategoryAsync(ICategoryService categoryService,
        int categoryId)
    {
        var response = await categoryService.DeleteCategoryAsync(categoryId);
        return TypedResults.Ok(response);
    }
}
