namespace BlazorECommerce.Client.Services.OrderServices;

public record OrderService(HttpClient HttpClient,
    IAuthService AuthService,
    NavigationManager NavigationManager) : IOrderService
{
    public async Task PlaceOrderAsync()
    {
        if (await AuthService.IsUserAuthenticatedAsync())
        {
            await HttpClient.PostAsync("api/order", null);
        }
        else
        {
            NavigationManager.NavigateTo("login");
        }
    }

    public async Task<List<OrderOverviewResponse>> GetOrdersAsync()
    {
        var result = await HttpClient.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponse>>>("api/order");
        return result?.Data ?? new List<OrderOverviewResponse>();
    }

    public async Task<OrderDetailsResponse> GetOrderDetailsAsync(int orderId)
    {
        var result = await HttpClient.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>($"api/order/{orderId}");
        return result?.Data ?? new OrderDetailsResponse();
    }
}
