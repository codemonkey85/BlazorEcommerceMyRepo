namespace BlazorECommerce.Rcl.Pages;

[Authorize]
// ReSharper disable once ClassNeverInstantiated.Global
public partial class UserProfile
{
    private readonly UserChangePassword request = new();
    private string message = string.Empty;

    private async Task ChangePasswordAsync()
    {
        var results = await AuthService.ChangePasswordAsync(request);
        message = results.Message;
    }
}
