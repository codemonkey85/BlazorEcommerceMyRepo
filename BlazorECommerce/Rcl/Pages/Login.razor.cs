namespace BlazorECommerce.Rcl.Pages;

public partial class Login
{
    private readonly UserLogin user = new();

    private string errorMessage = string.Empty;

    private string returnUrl = string.Empty;

    protected override void OnInitialized()
    {
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("returnUrl", out var url) && url is { Count: > 0 })
        {
            returnUrl = url!;
        }
    }

    private async Task HandleLoginAsync()
    {
        var result = await AuthService.LoginAsync(user);
        if (result is { Success: true })
        {
            errorMessage = string.Empty;
            await LocalStorageService.SetItemAsync("authToken", result.Data);
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            await CartService.StoreCartItemsAsync(true);
            await CartService.GetCartItemsCountAsync();
            NavigationManager.NavigateTo(returnUrl);
        }
        else
        {
            errorMessage = result.Message;
        }
    }
}
