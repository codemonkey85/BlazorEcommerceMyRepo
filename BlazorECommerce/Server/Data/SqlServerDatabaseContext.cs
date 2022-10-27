namespace BlazorECommerce.Server.Data;

public class SqlServerDatabaseContext : DatabaseContext
{
    public SqlServerDatabaseContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
    {
    }
}
