namespace BlazorECommerce.Client.Services.AuthServices;

public record AuthService(HttpClient HttpClient, AuthenticationStateProvider AuthStateProvider) : IAuthService
{
    public async Task<ServiceResponse<int>> RegisterAsync(UserRegister request)
    {
        var results = await HttpClient.PostAsJsonAsync("api/auth/register", request);
        return await results.Content.ReadFromJsonAsync<ServiceResponse<int>>() ?? new ServiceResponse<int>();
    }

    public async Task<ServiceResponse<string>> LoginAsync(UserLogin request)
    {
        var results = await HttpClient.PostAsJsonAsync("api/auth/login", request);
        return await results.Content.ReadFromJsonAsync<ServiceResponse<string>>() ?? new ServiceResponse<string>();
    }

    public async Task<ServiceResponse<bool>> ChangePasswordAsync(UserChangePassword request)
    {
        var results = await HttpClient.PostAsJsonAsync("api/auth/changepassword", request.Password);
        return await results.Content.ReadFromJsonAsync<ServiceResponse<bool>>() ?? new ServiceResponse<bool>();
    }

    public async Task<bool> IsUserAuthenticatedAsync() => await AuthStateProvider.GetAuthenticationStateAsync() is
    { User.Identity.IsAuthenticated: true };
}
