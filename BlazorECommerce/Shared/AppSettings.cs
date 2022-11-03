namespace BlazorECommerce.Shared;

public class AppSettings
{
    public AuthSettings AuthSettings { get; set; } = new();

    public StripeSettings StripeSettings { get; set; } = new();

    public DbProvider? DbProvider { get; set; }
}
