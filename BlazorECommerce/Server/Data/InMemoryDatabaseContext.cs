namespace BlazorECommerce.Server.Data;

public class InMemoryDatabaseContext : DatabaseContext
{
    public InMemoryDatabaseContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {
    }
}
