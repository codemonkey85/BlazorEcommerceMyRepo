namespace BlazorECommerce.Rcl.Pages;

public partial class OrderDetails
{
    [Parameter]
    public int OrderId { get; set; }

    private OrderDetailsResponse? order;

    protected override async Task OnInitializedAsync() =>
        order = await OrderService.GetOrderDetailsAsync(OrderId);
}
