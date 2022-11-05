namespace BlazorECommerce.Server.Data;

public class AzureCosmosDatabaseContext : DatabaseContext
{
    public AzureCosmosDatabaseContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {
    }
}
