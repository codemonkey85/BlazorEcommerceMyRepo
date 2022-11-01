namespace BlazorECommerce.Rcl.Pages;

[Authorize]
public partial class UserProfile
{
    private readonly UserChangePassword request = new();
    private string message = string.Empty;

    private async Task ChangePasswordAsync()
    {
        var result = await AuthService.ChangePasswordAsync(request);
        message = result.Message;
    }
}
