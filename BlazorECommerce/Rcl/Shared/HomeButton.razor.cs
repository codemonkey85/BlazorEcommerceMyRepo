namespace BlazorECommerce.Rcl.Shared;

public partial class HomeButton
{
    private void GoToHome() => NavigationManager.NavigateTo(string.Empty);
}
