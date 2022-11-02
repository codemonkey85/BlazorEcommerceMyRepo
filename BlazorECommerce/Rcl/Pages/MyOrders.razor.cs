namespace BlazorECommerce.Rcl.Pages;

public partial class MyOrders
{
    private List<OrderOverviewResponse>? orders;

    protected override async Task OnInitializedAsync() =>
        orders = await OrderService.GetOrdersAsync();
}
