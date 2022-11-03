namespace BlazorECommerce.Rcl.Pages;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Register
{
    private readonly UserRegister user = new();

    private string message = string.Empty;
    private string messageCssClass = string.Empty;

    private async Task HandleRegistration()
    {
        var results = await AuthService.RegisterAsync(user);
        message = results.Message;
        messageCssClass = results.Success ? "text-success" : "text-danger";
        if (results.Success)
        {
            NavigationManager.NavigateTo("login");
        }
    }
}
