namespace BlazorECommerce.Server.Data;

public class SqliteDatabaseContext : DatabaseContext
{
    public SqliteDatabaseContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {
    }
}
