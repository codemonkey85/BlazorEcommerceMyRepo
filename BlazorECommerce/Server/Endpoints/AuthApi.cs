namespace BlazorECommerce.Server.Endpoints;

public static class AuthApi
{
    public static IEndpointRouteBuilder MapAuthApi(this IEndpointRouteBuilder apiGroup)
    {
        var authGroup = apiGroup.MapGroup(Constants.Auth);

        authGroup.MapPost(Constants.Register, RegisterUserAsync);
        authGroup.MapPost(Constants.Login, LogInAsync);
        authGroup.MapPost(Constants.ChangePassword, ChangePasswordAsync);

        return apiGroup;
    }

    private static async Task<Results<Ok<ServiceResponse<int>>, BadRequest<ServiceResponse<int>>>> RegisterUserAsync(
        IAuthService authService, UserRegister request)
    {
        var response = await authService.RegisterAsync(new User { Email = request.Email }, request.Password);
        return !response.Success ? TypedResults.BadRequest(response) : TypedResults.Ok(response);
    }

    private static async Task<Results<Ok<ServiceResponse<string>>, BadRequest<ServiceResponse<string>>>> LogInAsync(
        IAuthService authService, UserLogin request)
    {
        var response = await authService.LogInAsync(request.Email, request.Password);
        return !response.Success ? TypedResults.BadRequest(response) : TypedResults.Ok(response);
    }

    [Authorize]
    private static async Task<Results<Ok<ServiceResponse<bool>>, BadRequest<ServiceResponse<bool>>>>
        ChangePasswordAsync(IAuthService authService, ClaimsPrincipal user, [FromBody] string newPassword)
    {
        var userId = authService.GetUserId();
        if (userId == 0)
        {
            return TypedResults.BadRequest(new ServiceResponse<bool>());
        }

        var response = await authService.ChangePasswordAsync(userId, newPassword);
        return !response.Success ? TypedResults.BadRequest(response) : TypedResults.Ok(response);
    }
}
