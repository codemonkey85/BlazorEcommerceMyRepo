namespace BlazorECommerce.Rcl.Pages;

public partial class OrderSuccess
{
    protected override async Task OnInitializedAsync() => await CartService.GetCartItemsCountAsync();
}
