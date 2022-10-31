namespace BlazorECommerce.Rcl.Components;

public partial class UserButton
{
    private bool showUserMenu = false;

    private string? UserMenuCssClass => showUserMenu ? "show-menu" : null;

    private void ToggleUserMenu() => showUserMenu = !showUserMenu;

    private async Task HideUserMenuAsync()
    {
        await Task.Delay(200);
        showUserMenu = false;
    }

    private async Task LogOutAsync()
    {
        await LocalStorageService.RemoveItemAsync("authToken");
        await AuthenticationStateProvider.GetAuthenticationStateAsync();
        NavigationManager.NavigateTo(string.Empty);
    }
}
