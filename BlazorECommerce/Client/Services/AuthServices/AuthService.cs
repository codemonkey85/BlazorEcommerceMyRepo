namespace BlazorECommerce.Client.Services.AuthServices;

public record AuthService(HttpClient HttpClient) : IAuthService
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
}
