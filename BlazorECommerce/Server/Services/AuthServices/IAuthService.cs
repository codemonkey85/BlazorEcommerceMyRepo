namespace BlazorECommerce.Server.Services.AuthServices;

public interface IAuthService
{
    Task<ServiceResponse<int>> RegisterAsync(User user, string password);

    Task<ServiceResponse<string>> LogInAsync(string email, string password);

    //Task<bool> UserExistsAsync(string email);
}
