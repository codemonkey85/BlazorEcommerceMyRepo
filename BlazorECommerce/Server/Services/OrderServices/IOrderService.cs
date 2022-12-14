namespace BlazorECommerce.Server.Services.OrderServices;

public interface IOrderService
{
    Task<ServiceResponse<bool>> PlaceOrderAsync(int userId);

    Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrdersAsync();

    Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetailsAsync(int orderId);
}
