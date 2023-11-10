namespace BlazorECommerce.Server.Data;

public class AzureCosmosDatabaseContext(DbContextOptions dbContextOptions) : DatabaseContext(dbContextOptions);
