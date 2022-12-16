// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
namespace BlazorECommerce.Shared;

// ReSharper disable once ClassNeverInstantiated.Global
public class StripeSettings
{
    public string ApiKey { get; set; } = string.Empty;

    public string SigningSecret { get; set; } = string.Empty;
}
