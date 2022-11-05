namespace BlazorECommerce.Shared;

public class AppSettings
{
    public AuthSettings AuthSettings { get; set; } = default!;

    public StripeSettings StripeSettings { get; set; } = default!;

    public AzureCosmosSettings AzureCosmosSettings { get; set; } = default!;

    public DbProvider? DbProvider { get; set; }
}
