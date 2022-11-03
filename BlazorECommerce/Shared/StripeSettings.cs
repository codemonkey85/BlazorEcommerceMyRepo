// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
namespace BlazorECommerce.Shared;

public class StripeSettings
{
    public string ApiKey { get; set; } = string.Empty;

    public string SigningSecret { get; set; } = string.Empty;
}
