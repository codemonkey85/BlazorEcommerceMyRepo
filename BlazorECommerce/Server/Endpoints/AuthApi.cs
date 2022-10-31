namespace BlazorECommerce.Server.Endpoints;

public static class AuthApi
{
    public static IEndpointRouteBuilder MapAuthApi(this IEndpointRouteBuilder apiGroup)
    {
        var authGroup = apiGroup.MapGroup("auth");

        authGroup.MapPost("register", RegisterUserAsync);
        authGroup.MapPost("login", LogInAsync);
        authGroup.MapPost("changepassword", ChangePasswordAsync);

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
        ChangePasswordAsync(ClaimsPrincipal user, [FromBody] string newPassword)
    {
        var principalUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(principalUserId, out var userId))
        {
            return TypedResults.BadRequest(new ServiceResponse<bool>());
        }

        // TODO: finish it
        return null;
    }
}
