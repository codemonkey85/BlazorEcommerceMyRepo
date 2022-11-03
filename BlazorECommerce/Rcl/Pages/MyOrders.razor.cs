namespace BlazorECommerce.Rcl.Pages;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class MyOrders
{
    private List<OrderOverviewResponse>? orders;

    protected override async Task OnInitializedAsync() =>
        orders = await OrderService.GetOrdersAsync();
}
