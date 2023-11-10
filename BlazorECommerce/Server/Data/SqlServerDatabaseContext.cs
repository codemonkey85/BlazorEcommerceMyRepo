namespace BlazorECommerce.Server.Data;

public class SqlServerDatabaseContext(DbContextOptions dbContextOptions) : DatabaseContext(dbContextOptions);
