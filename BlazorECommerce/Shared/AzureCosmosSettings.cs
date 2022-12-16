namespace BlazorECommerce.Shared;

// ReSharper disable once ClassNeverInstantiated.Global
public class AzureCosmosSettings
{
    public string AccountEndpoint { get; set; } = string.Empty;

    public string AccountKey { get; set; } = string.Empty;

    public string DatabaseName { get; set; } = string.Empty;
}
