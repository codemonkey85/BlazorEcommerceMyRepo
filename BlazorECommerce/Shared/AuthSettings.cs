namespace BlazorECommerce.Shared;

public class AuthSettings
{
    public string AuthToken { get; set; } = string.Empty;

    public int DaysToExpire { get; set; }

    public int RefreshTokenDaysToExpire { get; set; }
}
