namespace BlazorECommerce.Server.Services.PaymentServices;

public interface IPaymentService
{
    Task<Session> CreateCheckoutSession();

    Task<ServiceResponse<bool>> FullfillOrderAsync(HttpRequest request);
}
