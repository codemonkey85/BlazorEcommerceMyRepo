namespace BlazorECommerce.Client.Services;

public record OrderService(HttpClient HttpClient,
    IAuthService AuthService) : IOrderService
{
    public async Task<string> PlaceOrderAsync()
    {
        if (!await AuthService.IsUserAuthenticatedAsync())
        {
            return "login";
        }

        var results = await HttpClient.PostAsync("api/payment/checkout", null);
        var url = await results.Content.ReadAsStringAsync();
        return url.Replace("\"", string.Empty).Replace("'", string.Empty);
    }

    public async Task<List<OrderOverviewResponse>> GetOrdersAsync()
    {
        var results =
            await HttpClient.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponse>>>($"api/{nameof(Order)}");
        return results?.Data ?? new List<OrderOverviewResponse>();
    }

    public async Task<OrderDetailsResponse> GetOrderDetailsAsync(int orderId)
    {
        var results =
            await HttpClient.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>($"api/{nameof(Order)}/{orderId}");
        return results?.Data ?? new OrderDetailsResponse();
    }
}
