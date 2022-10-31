namespace BlazorECommerce.Rcl.Pages;

public partial class Register
{
    private readonly UserRegister user = new();

    private string message = string.Empty;
    private string messageCssClass = string.Empty;

    private async Task HandleRegistration()
    {
        var result = await AuthService.RegisterAsync(user);
        message = result.Message;
        messageCssClass = result.Success ? "text-success" : "text-danger";
        if (result.Success)
        {
            NavigationManager.NavigateTo("login");
        }
    }
}
