namespace BlazorECommerce.Shared.Services.OrderServices;

public interface IOrderService
{
    Task<string> PlaceOrderAsync();

    Task<List<OrderOverviewResponse>> GetOrdersAsync();

    Task<OrderDetailsResponse> GetOrderDetailsAsync(int orderId);
}
