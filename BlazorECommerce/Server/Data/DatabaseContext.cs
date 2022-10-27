namespace BlazorECommerce.Server.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
}
