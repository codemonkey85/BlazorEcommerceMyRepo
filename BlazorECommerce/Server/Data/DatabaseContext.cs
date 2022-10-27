namespace BlazorECommerce.Server.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

    public virtual DbSet<Product> Products { get; set; } = default!;
}
