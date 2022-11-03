namespace BlazorECommerce.Shared;

public class AuthSettings
{
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    public string AuthToken { get; set; } = string.Empty;

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public int DaysToExpire { get; set; }

    public int RefreshTokenDaysToExpire { get; set; }
}
