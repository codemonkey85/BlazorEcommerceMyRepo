namespace BlazorECommerce.Client.Services;

public record AuthService(HttpClient HttpClient, AuthenticationStateProvider AuthStateProvider) : IAuthService
{
    public async Task<ServiceResponse<int>> RegisterAsync(UserRegister request)
    {
        var results = await HttpClient.PostAsJsonAsync($"{Constants.AuthRegisterApi}", request);
        return await results.Content.ReadFromJsonAsync<ServiceResponse<int>>() ?? new ServiceResponse<int>();
    }

    public async Task<ServiceResponse<string>> LoginAsync(UserLogin request)
    {
        var results = await HttpClient.PostAsJsonAsync($"{Constants.AuthLoginApi}", request);
        return await results.Content.ReadFromJsonAsync<ServiceResponse<string>>() ?? new ServiceResponse<string>();
    }

    public async Task<ServiceResponse<bool>> ChangePasswordAsync(UserChangePassword request)
    {
        var results = await HttpClient.PostAsJsonAsync($"{Constants.AuthChangePasswordApi}", request.Password);
        return await results.Content.ReadFromJsonAsync<ServiceResponse<bool>>() ?? new ServiceResponse<bool>();
    }

    public async Task<bool> IsUserAuthenticatedAsync() => await AuthStateProvider.GetAuthenticationStateAsync() is
    { User.Identity.IsAuthenticated: true };
}
