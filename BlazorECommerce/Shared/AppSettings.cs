namespace BlazorECommerce.Shared;

public class AppSettings
{
    public AuthSettings AuthSettings { get; set; } = new();

    public DbProvider? DbProvider { get; set; } = null;
}
