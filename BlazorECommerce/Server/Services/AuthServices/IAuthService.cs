namespace BlazorECommerce.Server.Services.AuthServices;

public interface IAuthService
{
    Task<ServiceResponse<int>> RegisterAsync(User user, string password);

    Task<ServiceResponse<string>> LogInAsync(string email, string password);

    Task<ServiceResponse<bool>> ChangePasswordAsync(int userId, string newPassword);

    bool IsUserInRole(string role);

    int GetUserId();

    string GetUserEmail();

    Task<User?> GetUserByEmailAsync(string email);
}
