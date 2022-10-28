namespace BlazorECommerce.Rcl.Shared;

public partial class ShopNavMenu
{
    private bool _collapseNavMenu = true;

    private string? NavMenuCssClass => _collapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu() => _collapseNavMenu = !_collapseNavMenu;

    protected override async Task OnInitializedAsync() => await CategoryService.GetCategoriesAsync();
}
