namespace BlazorECommerce.Rcl.Pages;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class OrderSuccess
{
    protected override async Task OnInitializedAsync() => await CartService.GetCartItemsCountAsync();
}
