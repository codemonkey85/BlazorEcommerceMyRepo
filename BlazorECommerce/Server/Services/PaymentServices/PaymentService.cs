namespace BlazorECommerce.Server.Services.PaymentServices;

public record PaymentService(
    ICartService CartService,
    IAuthService AuthService,
    IOrderService OrderService,
    AppSettings AppSettings) : IPaymentService
{
    public async Task<Session> CreateCheckoutSession()
    {
        var products = (await CartService.GetDbCartProductsAsync()).Data;
        if (products is null)
        {
            return new Session();
        }

        StripeConfiguration.ApiKey = AppSettings.StripeSettings.ApiKey;
        var lineItems = new List<SessionLineItemOptions>();
        products.ForEach(product => lineItems.Add(new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmountDecimal = product.Price * 100,
                Currency = "usd",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = product.Title,
                    Images = new List<string> { product.ImageUrl }
                }
            },
            Quantity = product.Quantity
        }));

        var options = new SessionCreateOptions
        {
            CustomerEmail = AuthService.GetUserEmail(),
            ShippingAddressCollection = new SessionShippingAddressCollectionOptions
            {
                AllowedCountries = new List<string> { "US" }
            },
            PaymentMethodTypes = new List<string>
            {
                "card"
            },
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = $"{Constants.ClientBaseUrl}/ordersuccess",
            CancelUrl = $"{Constants.ClientBaseUrl}/cart"
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);
        return session;
    }

    public async Task<ServiceResponse<bool>> FullfillOrderAsync(HttpRequest request)
    {
        var json = await new StreamReader(request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json, request.Headers["Stripe-Signature"],
                AppSettings.StripeSettings.SigningSecret);
            if (stripeEvent.Type != Events.CheckoutSessionCompleted)
            {
                return new ServiceResponse<bool>(true);
            }

            if (stripeEvent.Data.Object is not Session session)
            {
                return new ServiceResponse<bool> { Data = false, Success = false, Message = "Session not found." };
            }

            var user = await AuthService.GetUserByEmailAsync(session.CustomerEmail);
            if (user is null)
            {
                return new ServiceResponse<bool> { Data = false, Success = false, Message = "User not found." };
            }

            await OrderService.PlaceOrderAsync(user.Id);

            return new ServiceResponse<bool>(true);
        }
        catch (StripeException stripeException)
        {
            return new ServiceResponse<bool> { Data = false, Success = false, Message = stripeException.Message };
        }
    }
}
