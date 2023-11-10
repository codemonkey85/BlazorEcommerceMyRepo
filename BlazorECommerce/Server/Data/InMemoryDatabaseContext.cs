namespace BlazorECommerce.Server.Data;

public class InMemoryDatabaseContext(DbContextOptions dbContextOptions) : DatabaseContext(dbContextOptions);
