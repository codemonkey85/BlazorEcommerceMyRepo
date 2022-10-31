namespace BlazorECommerce.Server.Endpoints;

public static class AuthApi
{
    public static IEndpointRouteBuilder MapAuthApi(this IEndpointRouteBuilder apiGroup)
    {
        var authGroup = apiGroup.MapGroup("auth");

        authGroup.MapPost("register", RegisterUser);
        authGroup.MapPost("login", LogInAsync);

        return apiGroup;
    }

    private static async Task<Results<Ok<ServiceResponse<int>>, BadRequest<ServiceResponse<int>>>> RegisterUser(
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
}
